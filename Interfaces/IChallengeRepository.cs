using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        Task<IEnumerable<Challenge>> GetActiveChallengesAsync();
        Task<IEnumerable<Challenge>> GetAllChallengesAsync();
    }
}
