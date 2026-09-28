using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AcessoController : ControllerBase
{
    [HttpGet]
    // Exige que o usuário cumpra a política "IdadeMinima" para acessar este endpoint.
    // Se a política não for cumprida ou não houver autenticação, o acesso será negado (401/403).
    [Authorize(Policy = "idadeMinima")]
    public IActionResult Get()
    {
        // Retorna HTTP 200 OK com a mensagem caso o acesso seja autorizado
        return Ok("Acesso permitido!");
    }
}
