using Microsoft.EntityFrameworkCore;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;

namespace CarePlusApi.Data.Repositories
{
    public class RewardClaimRepository : Repository<RewardClaim>, IRewardClaimRepository
    {
        public RewardClaimRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RewardClaim>> GetClaimsByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(rc => rc.UserId == userId)
                .Include(rc => rc.Reward)
                .ToListAsync();
        }
    }
}
