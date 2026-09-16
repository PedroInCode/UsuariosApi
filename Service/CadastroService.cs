using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Service;

/// <summary>
/// Camada de serviço responsável por orquestrar e isolar as regras de negócio 
/// relativas ao processo de cadastro de novos usuários.
/// </summary>
public class CadastroService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;

    /// <summary>
    /// Construtor que recebe as dependências necessárias via Injeção de Dependência.
    /// </summary>
    /// <param name="mapper">Instância do AutoMapper para conversão de DTOs em Entidades.</param>
    /// <param name="userManager">Gerenciador nativo do ASP.NET Core Identity para criação e gerenciamento de usuários no banco de dados.</param>
    public CadastroService(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <summary>
    /// Converte os dados do DTO para a entidade de domínio e executa a persistência do usuário 
    /// no banco de dados utilizando as regras e algoritmos de criptografia de senha do Identity.
    /// </summary>
    /// <param name="dto">Objeto contendo as informações de cadastro recebidas do Controller.</param>
    /// <exception cref="ApplicationException">Lançada caso o processo de criação falhe pelas regras do Identity (ex: senha fora do padrão ou usuário duplicado).</exception>
    public async Task Cadastra(CreateUsuarioDto dto)
    {
        // Converte o DTO recebido para o modelo de domínio Usuario
        Usuario usuario = _mapper.Map<Usuario>(dto);

        // Cria o usuário no banco de dados com a senha criptografada (hash automático)
        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        // Se o Identity recusar a criação (ex: senha muito simples), lança exceção
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }
    }
}