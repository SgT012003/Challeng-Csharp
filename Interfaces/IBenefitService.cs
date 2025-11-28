using CarePlusApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarePlusApi.Interfaces
{
    public interface IBenefitService
    {
        Task<IEnumerable<BenefitResponseDto>> GetBenefitsByUserIdAsync(Guid userId);
        Task<BenefitResponseDto> ClaimRewardAsync(Guid userId, Guid rewardId);
    }
}
