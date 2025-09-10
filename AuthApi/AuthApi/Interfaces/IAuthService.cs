using AuthApi.DTOs.UsuarioDTOs;

namespace AuthApi.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioRespuestaDto> RegistrarAsync(UsuarioRegistroDto dto);
        Task<UsuarioRespuestaDto?> LoginAsync(UsuarioLoginDto dto);
    }
}
