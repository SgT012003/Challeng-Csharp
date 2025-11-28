using Microsoft.EntityFrameworkCore;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;

namespace CarePlusApi.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetTopRankedUsersAsync(int topN)
        {
            return await _dbSet
                .OrderByDescending(u => u.Pontos)
                .Take(topN)
                .ToListAsync();
        }
    }
}
