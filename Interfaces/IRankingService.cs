using CarePlusApi.DTOs;

namespace CarePlusApi.Interfaces
{
    public interface IRankingService
    {
        Task<IEnumerable<RankingResponseDto>> GetRankingAsync();
    }
}
