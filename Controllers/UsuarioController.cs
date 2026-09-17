using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Service;

namespace UsuariosApi.Controllers;

/// 
/// Controller responsável pela gestão dos endpoints de usuários.
/// Atua como porta de entrada (gatekeeper) para as requisições HTTP, delegando as regras de negócio para a camada de serviço.
/// 
[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    /// 
    /// Construtor que recebe as dependências via injeção de dependência.
    /// 
    /// Serviço encarregado do fluxo e regras de cadastro de usuários.
    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Endpoint responsável pelo cadastro de um novo usuário.
    /// Rota: POST /usuario/cadastro
    /// </summary>
    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario([FromBody] CreateUsuarioDto dto)
    {
        await _usuarioService.Cadastra(dto);
        return Ok("Usuário cadastrado com sucesso!");
    }

    /// <summary>
    /// Endpoint responsável pela autenticação de um usuário.
    /// Rota: POST /usuario/login
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto)
    {
        var token = await _usuarioService.Login(dto);
        return Ok(token);
    }
}