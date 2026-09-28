using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization;

public class IdadeMinima : IAuthorizationRequirement
{
    public int _idade { get; set; }
    public IdadeMinima(int idade)
    {
        this._idade = idade;
    }
}
