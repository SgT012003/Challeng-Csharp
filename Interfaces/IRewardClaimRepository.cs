using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IRewardClaimRepository : IRepository<RewardClaim>
    {
        Task<IEnumerable<RewardClaim>> GetClaimsByUserIdAsync(Guid userId);
    }
}
