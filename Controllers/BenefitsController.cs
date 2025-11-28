using Microsoft.AspNetCore.Mvc;
using CarePlusApi.Interfaces;
using CarePlusApi.DTOs;

namespace CarePlusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenefitsController : ControllerBase
    {
        private readonly IBenefitService _benefitService;

        public BenefitsController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }

        /// <summary>
        /// Lista os benefícios (Rewards) disponíveis e se o usuário já os resgatou.
        /// </summary>
        /// <param name="userId">ID do usuário para verificar resgates.</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BenefitResponseDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBenefits([FromQuery] Guid userId)
        {
            // Em um cenário real, o userId viria do token de autenticação
            var benefits = await _benefitService.GetBenefitsByUserIdAsync(userId);
            return Ok(benefits);
        }

        /// <summary>
        /// Resgata um benefício (Reward) para o usuário.
        /// </summary>
        /// <param name="userId">ID do usuário que está resgatando.</param>
        /// <param name="rewardId">ID do benefício a ser resgatado.</param>
        [HttpPost("{rewardId}/claim")]
        [ProducesResponseType(typeof(BenefitResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> ClaimBenefit(Guid rewardId, [FromQuery] Guid userId)
        {
            var claimedBenefit = await _benefitService.ClaimRewardAsync(userId, rewardId);
            return Ok(claimedBenefit);
        }
    }
}
