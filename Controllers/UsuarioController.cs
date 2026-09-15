using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IMapper _mapper;
    private UserManager<Usuario> _userManager;

    public UsuarioController(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }


    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);

        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        if (resultado.Succeeded) return Ok("Usuário Cadastrado!");

        //throw new ApplicationException("Falha ao cadastrar o usuário!");
        return BadRequest(resultado.Errors);
    }
}
