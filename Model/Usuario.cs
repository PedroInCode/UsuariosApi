using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Model;

/// 
/// Representa a entidade de usuário no sistema, estendendo as funcionalidades nativas do ASP.NET Core Identity.
/// Herda de , que já fornece campos padrão como Id (string/GUID), Username, Email, PasswordHash, etc.
/// 
public class Usuario : IdentityUser
{
    /// 
    /// Data de nascimento do usuário. Campo customizado adicionado além das propriedades padrão do Identity.
    /// 
    public DateTime DataNascimento { get; set; }

    /// 
    /// Construtor padrão da classe Usuario.
    /// Chama o construtor da classe base () para garantir a inicialização correta das propriedades nativas.
    /// 
    public Usuario() : base() { }
}
