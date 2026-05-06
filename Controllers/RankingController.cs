using Microsoft.AspNetCore.Mvc;
using CarePlusApi.Interfaces;
using CarePlusApi.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CarePlusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        /// <summary>
        /// Obtém o ranking dos usuários baseado na pontuação total.
        /// </summary>
        /// <returns>Lista de usuários ranqueados.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RankingResponseDto>), 200)]
        public async Task<IActionResult> GetRanking()
        {
            var ranking = await _rankingService.GetRankingAsync();
            return Ok(ranking);
        }
    }
}
