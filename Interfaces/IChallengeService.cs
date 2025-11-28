using CarePlusApi.DTOs;
using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IChallengeService
    {
        // CRUD de Challenges
        Task<ChallengeResponseDto> CreateChallengeAsync(CreateChallengeDto dto);
        Task<IEnumerable<ChallengeResponseDto>> GetChallengesAsync(bool includeDeleted = false);
        Task<ChallengeResponseDto> GetChallengeByIdAsync(Guid id);
        Task<ChallengeResponseDto> UpdateChallengeAsync(Guid id, UpdateChallengeDto dto);
        Task DeleteChallengeAsync(Guid id); // Exclusão lógica

        // Progresso do Usuário
        Task<UserChallengeResponseDto> UpdateUserChallengeProgressAsync(Guid challengeId, UpdateUserChallengeProgressDto dto);
    }
}
