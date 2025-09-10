using AuthApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using AuthApi.Entidades;

namespace AuthApi.Repositorios
{
    
   public class CategoriaEfRepository : ICategoriaEfRepository
    {
        private readonly AppDbContext _context;

        public CategoriaEfRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoriaef>> GetAllAsync()
            => await _context.Categoria.AsNoTracking().ToListAsync();

        public async Task<Categoriaef?> GetByIdAsync(int id)
            => await _context.Categoria.FindAsync(id);

        public async Task<Categoriaef> AddAsync(Categoriaef entity)
        {
            _context.Categoria.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Categoriaef entity)
        {
            _context.Categoria.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Categoria.FindAsync(id);
            if (existing == null) return false;
            _context.Categoria.Remove(existing);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
