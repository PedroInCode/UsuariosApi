using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos;

/// 
/// Objeto de Transferência de Dados (DTO) utilizado para capturar e validar 
/// os dados enviados pelo cliente no momento da criação de um novo usuário.
/// 
public class CreateUsuarioDto
{
    /// 
    /// Nome de usuário escolhido para login na aplicação.
    /// 
    [Required(ErrorMessage = "O campo de usuário é obrigatório.")]
    public string Username { get; set; } = string.Empty;

    /// 
    /// Data de nascimento do usuário para controle de idade e perfil.
    /// 
    [Required(ErrorMessage = "O campo de data de nascimento é obrigatório.")]
    public DateTime DataNascimento { get; set; }

    /// 
    /// Senha de acesso escolhida pelo usuário.
    /// 
    [Required(ErrorMessage = "O campo de senha é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// 
    /// Confirmação da senha para evitar erros de digitação. 
    /// Deve ser idêntica ao campo .
    /// 
    [Required(ErrorMessage = "O campo de confirmação de senha é obrigatório.")]
    [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
    public string RePassword { get; set; } = string.Empty;
}