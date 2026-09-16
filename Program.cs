using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

// Registra os controllers e a documentação do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseAuthorization();
app.MapControllers();
app.Run();