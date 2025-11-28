using Microsoft.AspNetCore.Mvc;
using CarePlusApi.Interfaces;
using CarePlusApi.DTOs;
using CarePlusApi.Models;

namespace CarePlusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengesController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        /// <summary>
        /// Cria um novo desafio.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ChallengeResponseDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateChallenge([FromBody] CreateChallengeDto dto)
        {
            var challenge = await _challengeService.CreateChallengeAsync(dto);
            return CreatedAtAction(nameof(GetChallengeById), new { id = challenge.Id }, challenge);
        }

        /// <summary>
        /// Lista desafios ativos ou todos (incluindo deletados logicamente).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChallengeResponseDto>), 200)]
        public async Task<IActionResult> GetChallenges([FromQuery] bool includeDeleted = false)
        {
            var challenges = await _challengeService.GetChallengesAsync(includeDeleted);
            return Ok(challenges);
        }

        /// <summary>
        /// Obtém um desafio pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ChallengeResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetChallengeById(Guid id)
        {
            var challenge = await _challengeService.GetChallengeByIdAsync(id);
            return Ok(challenge);
        }

        /// <summary>
        /// Atualiza os dados de um desafio.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ChallengeResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateChallenge(Guid id, [FromBody] UpdateChallengeDto dto)
        {
            var challenge = await _challengeService.UpdateChallengeAsync(id, dto);
            return Ok(challenge);
        }

        /// <summary>
        /// Deleta logicamente um desafio (seta IsDeleted=true e Status=Deleted).
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteChallenge(Guid id)
        {
            await _challengeService.DeleteChallengeAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Atualiza o progresso de um usuário em um desafio específico.
        /// </summary>
        [HttpPost("{challengeId}/progress")]
        [ProducesResponseType(typeof(UserChallengeResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> UpdateProgress(Guid challengeId, [FromBody] UpdateUserChallengeProgressDto dto)
        {
            var userChallenge = await _challengeService.UpdateUserChallengeProgressAsync(challengeId, dto);
            return Ok(userChallenge);
        }
    }
}
