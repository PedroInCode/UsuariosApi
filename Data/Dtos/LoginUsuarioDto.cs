using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos;

/// <summary>
/// Objeto de Transferência de Dados (DTO) utilizado para capturar 
/// as credenciais de acesso durante a autenticação do usuário.
/// </summary>
public class LoginUsuarioDto
{
    /// <summary>
    /// Nome de usuário para autenticação.
    /// </summary>
    [Required(ErrorMessage = "O campo de usuário é obrigatório.")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Senha de acesso do usuário.
    /// </summary>
    [Required(ErrorMessage = "O campo de senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}