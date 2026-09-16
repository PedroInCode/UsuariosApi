using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Model;

namespace UsuariosApi.Data;

/// 
/// Contexto de banco de dados do Entity Framework Core responsável pela persistência dos dados de usuários.
/// Herda de  para mapear automaticamente todas as tabelas nativas 
/// de autenticação do ASP.NET Core Identity (ex: AspNetUsers, AspNetRoles, AspNetUserClaims) usando a entidade personalizada .
/// 
public class UsuarioDbContext : IdentityDbContext
{
    /// 
    /// Construtor do DbContext que recebe as opções de configuração do banco (ex: string de conexão e provedor MySQL).
    /// 
    /// Opções de configuração repassadas ao construtor da classe base .
    public UsuarioDbContext(DbContextOptions opts) : base(opts) { }
}