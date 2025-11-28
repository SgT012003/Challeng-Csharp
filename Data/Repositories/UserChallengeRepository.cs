using Microsoft.EntityFrameworkCore;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;

namespace CarePlusApi.Data.Repositories
{
    public class UserChallengeRepository : Repository<UserChallenge>, IUserChallengeRepository
    {
        public UserChallengeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserChallenge?> GetByUserIdAndChallengeIdAsync(Guid userId, Guid challengeId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);
        }

        public async Task<IEnumerable<UserChallenge>> GetCompletedChallengesByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(uc => uc.UserId == userId && uc.Completed)
                .ToListAsync();
        }
    }
}
