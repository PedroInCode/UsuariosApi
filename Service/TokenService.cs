using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Model;

namespace UsuariosApi.Service;

/// <summary>
/// Serviço responsável pela geração e validação de Tokens JWT (JSON Web Tokens).
/// </summary>
internal class TokenService
{
    /// <summary>
    /// Gera um token JWT assinado contendo as reivindicações (claims) do usuário.
    /// </summary>
    /// <param name="usuario">Entidade do usuário autenticado.</param>
    /// <returns>Uma string representando o Token JWT no formato encoded.</returns>
    public void GenerateToken(Usuario usuario)
    {
        // 1. Dados gravados no "crachá" (Claims)
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id),
            new Claim(ClaimTypes.DateOfBirth, 
            usuario.DataNascimento.ToString())
        };

        // 2. Chave secreta de assinatura (mínimo de 256 bits/32 caracteres para HMAC-SHA256)
        // obs: Como é uma api de estudos, não vou esconder essa chave! 
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9ASHDA98H9ah9ha9H9A89n0f"));

        // 3. Algoritmo de criptografia para assinar o token
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        // 4. Montagem da estrutura do Token JWT
        var token = new JwtSecurityToken
            (
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
            );
    }
}