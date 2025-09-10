using AuthApi.Entidades;

namespace AuthApi.Interfaces
{
    public interface ICategoriaEfRepository
    {
        Task<List<Categoriaef>> GetAllAsync();
        Task<Categoriaef?> GetByIdAsync(int id);
        Task<Categoriaef> AddAsync(Categoriaef entity);
        Task<bool> UpdateAsync(Categoriaef entity);
        Task<bool> DeleteAsync(int id);
    }
}
