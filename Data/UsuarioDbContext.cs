using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Model;

namespace UsuariosApi.Data;

/// <summary>
/// Contexto de banco de dados do Entity Framework Core responsável pela persistência dos dados de usuários.
/// Herda de <see cref="IdentityDbContext{TUser}"/> para mapear automaticamente todas as tabelas nativas 
/// de autenticação do ASP.NET Core Identity.
/// </summary>
public class UsuarioDbContext : IdentityDbContext<Usuario>
{
    /// <summary>
    /// Construtor do DbContext que recebe as opções de configuração do banco.
    /// </summary>
    /// <param name="opts">Opções de configuração repassadas ao construtor da classe base.</param>
    public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) : base(opts) { }

    /// <summary>
    /// Configura o mapeamento dos modelos e tabelas no banco de dados.
    /// É fundamental chamar o base.OnModelCreating(builder) para registrar as entidades nativas do Identity.
    /// </summary>
    /// <param name="builder">Construtor de modelos do Entity Framework.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}