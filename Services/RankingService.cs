using CarePlusApi.DTOs;
using CarePlusApi.Interfaces;
using AutoMapper;

namespace CarePlusApi.Services
{
    public class RankingService : IRankingService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public RankingService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RankingResponseDto>> GetRankingAsync()
        {
            // Pega os 100 usuários com maior pontuação
            var topUsers = await _usuarioRepository.GetTopRankedUsersAsync(100);

            var rankingList = new List<RankingResponseDto>();
            int position = 1;

            foreach (var user in topUsers)
            {
                rankingList.Add(new RankingResponseDto
                {
                    Position = position++,
                    UserId = user.Id,
                    NomeUsuario = user.Nome,
                    PontuacaoTotal = user.Pontos
                });
            }

            return rankingList;
        }
    }
}
