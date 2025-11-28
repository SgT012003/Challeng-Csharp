using Microsoft.EntityFrameworkCore;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;

namespace CarePlusApi.Data.Repositories
{
    public class RewardRepository : Repository<Reward>, IRewardRepository
    {
        public RewardRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reward>> GetAvailableRewardsAsync(int userPoints)
        {
            return await _dbSet
                .Where(r => r.CostPoints <= userPoints)
                .OrderBy(r => r.CostPoints)
                .ToListAsync();
        }
    }
}
