using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Model;
using UsuariosApi.Service;

/// <summary>
/// Ponto de entrada (Entry Point) da aplicação ASP.NET Core.
/// Configura o container de injeção de dependências e os middlewares HTTP.
/// </summary>

var builder = WebApplication.CreateBuilder(args);

// ===================================================================================
// 1. CONFIGURAÇÃO DE SERVIÇOS (Injeção de Dependência)
// ===================================================================================

// Recupera a string de conexão configurada no appsettings.json
var connString = builder.Configuration.GetConnectionString("UsuariosConnection");

// Configura o Entity Framework Core com o provedor MySQL (Pomelo)
builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    // AutoDetect identifica automaticamente a versão do MySQL rodando no servidor
    opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
});

// Configura o ASP.NET Core Identity para gestão de usuários e credenciais
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>() // Vincula o Identity ao DbContext do projeto
    .AddDefaultTokenProviders();                   // Habilita suporte a geração de tokens do sistema

// Configura o AutoMapper mapeando todos os Profiles existentes na aplicação
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registra a camada de serviço com ciclo de vida AddScoped (uma instância por requisição HTTP)
builder.Services.AddScoped<UsuarioService>();

// Registra a camada de serviço com ciclo de vida AddScoped (uma instância por requisição HTTP)
builder.Services.AddScoped<TokenService>();

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

// Registra os controllers e a documentação do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra e configura o serviço de Autenticação da aplicação
builder.Services.AddAuthentication(opts =>
{
    // Define que o esquema padrão de autenticação do sistema será via JWT Bearer (Token)
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    // Define os parâmetros de validação que o servidor usará para checar cada Token recebido
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        // Exige que a chave de assinatura do Token seja validada para garantir que ele não foi adulterado
        ValidateIssuerSigningKey = true,

        // Define a chave secreta simétrica (com no mínimo 32 caracteres / 256 bits) usada para validar a assinatura
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9ASHDA98H9ah9ha9H9A89n0f_12345678")),

        // Desabilita a validação da Audiência (quem deve consumir o token) por se tratar de um ambiente de estudos
        ValidateAudience = false,

        // Desabilita a validação do Emissor (quem gerou o token) por se tratar de um ambiente de estudos
        ValidateIssuer = false,

        // Zera a margem de tolerância do relógio para expiração (por padrão o .NET dá 5 min extra; zerando, o token expira na hora)
        ClockSkew = TimeSpan.Zero
    };
});

// Adiciona os serviços de autorização à aplicação
builder.Services.AddAuthorization(opts =>
// Define uma nova política de acesso chamada "IdadeMinima"
opts.AddPolicy("IdadeMinima", policy =>
// Associa o requisito de autorização a esta política,
// definindo que o valor exigido para acesso é 18 anos.
policy.AddRequirements(new IdadeMinima(18))));

// ===================================================================================
// 2. PIPELINE DE REQUISIÇÕES HTTP (Middlewares)
// ===================================================================================

var app = builder.Build();

// Habilita a interface do Swagger em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Habilita o "segurança" da aplicação: intercepta as requisições e descobre QUEM é o usuário através do Token
app.UseAuthentication();

// Habilita as regras de autorização: decide O QUE o usuário autenticado pode ou não acessar
app.UseAuthorization();
app.MapControllers();
app.Run();