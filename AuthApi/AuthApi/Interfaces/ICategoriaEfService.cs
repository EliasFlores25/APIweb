using AuthApi.DTOs.CategoriaDTOs;

namespace AuthApi.Interfaces
{
    public interface ICategoriaEfService
    {
        Task<List<CategoriaRespuestaEfDto>> GetAllAsync();
        Task<CategoriaRespuestaEfDto?> GetByIdAsync(int id);
        Task<CategoriaRespuestaEfDto> CreateAsync(CategoriaCrearEfDto dto);
        Task<bool> UpdateAsync(int id, CategoriaActualizarEfDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
