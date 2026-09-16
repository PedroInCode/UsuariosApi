using AutoMapper;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Profiles;

/// 
/// Perfil de configuração do AutoMapper responsável por mapear as conversões 
/// de objetos entre DTOs e a entidade .
/// 
public class UsuarioProfile : Profile
{
    /// 
    /// Inicializa uma nova instância do  
    /// definindo as regras de mapeamento de tipos da aplicação.
    /// 
    public UsuarioProfile()
    {
        // Define o mapeamento de entrada: converte os dados do DTO para a entidade Usuario
        CreateMap<CreateUsuarioDto, Usuario>();
    }
}