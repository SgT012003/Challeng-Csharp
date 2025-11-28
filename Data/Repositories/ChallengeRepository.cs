using Microsoft.EntityFrameworkCore;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;

namespace CarePlusApi.Data.Repositories
{
    public class ChallengeRepository : Repository<Challenge>, IChallengeRepository
    {
        public ChallengeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Challenge>> GetActiveChallengesAsync()
        {
            return await _dbSet
                .Where(c => c.Status == ChallengeStatus.Go && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Challenge>> GetAllChallengesAsync()
        {
            // Remove o filtro de exclusão lógica para listar todos
            return await _context.Challenges.IgnoreQueryFilters().ToListAsync();
        }
    }
}
