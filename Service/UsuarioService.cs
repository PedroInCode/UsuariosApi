using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Service;

/// <summary>
/// Camada de serviço responsável por orquestrar e isolar as regras de negócio 
/// relativas ao processo de cadastro de novos usuários.
/// </summary>
public class UsuarioService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly TokenService _tokenService;

    /// <summary>
    /// Construtor que recebe as dependências necessárias via Injeção de Dependência.
    /// </summary>
    /// <param name="mapper">Instância do AutoMapper para conversão de DTOs em Entidades.</param>
    /// <param name="userManager">Gerenciador nativo do ASP.NET Core Identity para criação e gerenciamento de usuários no banco de dados.</param>
    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, 
        SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
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

    /// <summary>
    /// Realiza a autenticação (login) do usuário no sistema verificando usuário e senha.
    /// </summary>
    /// <param name="dto">Credenciais de acesso (Username e Password).</param>
    /// <exception cref="ApplicationException">Lançada caso as credenciais sejam inválidas.</exception>
    public async Task<string> Login(LoginUsuarioDto dto)
    {
        // O 3º parâmetro (isPersistent) indica se o login deve persistir em cookie após fechar o navegador (false).
        // O 4º parâmetro (lockoutOnFailure) indica se deve bloquear a conta após tentativas erradas (false).
        var resultado = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!resultado.Succeeded)
            throw new ApplicationException("Usuário não autenticado!");

        var usuario = _signInManager.
            UserManager.
            Users.
            FirstOrDefault(user => user.UserName == dto.UserName.ToUpper());

        var token = _tokenService.GenerateToken(usuario);

        return token;
    }
}