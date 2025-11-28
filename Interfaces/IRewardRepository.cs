using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IRewardRepository : IRepository<Reward>
    {
        Task<IEnumerable<Reward>> GetAvailableRewardsAsync(int userPoints);
    }
}
