using AuthApi.Interfaces;
using AuthApi.DTOs.CategoriaDTOs;
using AuthApi.Entidades;

namespace AuthApi.Servicios
{
    public class CategoriaEfService : ICategoriaEfService
    {
        private readonly ICategoriaEfRepository _repo;

        public CategoriaEfService(ICategoriaEfRepository repo) => _repo = repo;

        public async Task<List<CategoriaRespuestaEfDto>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(x => new CategoriaRespuestaEfDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion
            }).ToList();

        public async Task<CategoriaRespuestaEfDto?> GetByIdAsync(int id)
        {
            var x = await _repo.GetByIdAsync(id);
            return x == null ? null : new CategoriaRespuestaEfDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion
            };
        }

        public async Task<CategoriaRespuestaEfDto> CreateAsync(CategoriaCrearEfDto dto)
        {
            var entity = new Categoriaef { Nombre = dto.Nombre.Trim(), Descripcion = dto.Descripcion.Trim() };
            var saved = await _repo.AddAsync(entity);
            return new CategoriaRespuestaEfDto { Id = saved.Id, Nombre = saved.Nombre, Descripcion = saved.Descripcion };
        }

        public async Task<bool> UpdateAsync(int id, CategoriaActualizarEfDto dto)
        {
            var current = await _repo.GetByIdAsync(id);
            if (current == null) return false;
            current.Nombre = dto.Nombre.Trim();
            current.Descripcion = dto.Descripcion.Trim();
            return await _repo.UpdateAsync(current);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
