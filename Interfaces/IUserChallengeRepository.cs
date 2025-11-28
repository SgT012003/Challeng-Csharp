using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IUserChallengeRepository : IRepository<UserChallenge>
    {
        Task<UserChallenge?> GetByUserIdAndChallengeIdAsync(Guid userId, Guid challengeId);
        Task<IEnumerable<UserChallenge>> GetCompletedChallengesByUserIdAsync(Guid userId);
    }
}
