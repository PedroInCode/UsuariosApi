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
    private readonly CadastroService _cadastroService;

    /// 
    /// Construtor que recebe as dependências via injeção de dependência.
    /// 
    /// Serviço encarregado do fluxo e regras de cadastro de usuários.
    public UsuarioController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    /// 
    /// Endpoint responsável pelo cadastro de um novo usuário na aplicação.
    /// 
    /// Objeto contendo as informações necessárias para criação do usuário (Username, DataNascimento, Passwords).
    /// Retorna um HTTP 200 (OK) com uma mensagem de confirmação em caso de sucesso.
    [HttpPost]
    public async Task<IActionResult> CadastraUsuario([FromBody] CreateUsuarioDto dto)
    {
        await _cadastroService.Cadastra(dto);
        return Ok("Usuário cadastrado com sucesso!");
    }
}