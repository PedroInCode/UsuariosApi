using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization;

// A interface 'IAuthorizationRequirement' indica ao ASP.NET que esta classe 
// funciona como um requisito/critério para uma política de autorização.
public class IdadeMinima : IAuthorizationRequirement
{
    // Propriedade que armazena a idade informada na configuração da política (ex: 18)
    public int _idade { get; set; }

    // Construtor que recebe a idade mínima necessária para cumprir a regra
    public IdadeMinima(int idade)
    {
        this._idade = idade;
    }
}
