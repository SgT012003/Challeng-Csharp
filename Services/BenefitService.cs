using CarePlusApi.DTOs;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;
using CarePlusApi.Exceptions;
using AutoMapper;

namespace CarePlusApi.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly IRewardClaimRepository _rewardClaimRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public BenefitService(IRewardRepository rewardRepository, IRewardClaimRepository rewardClaimRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _rewardRepository = rewardRepository;
            _rewardClaimRepository = rewardClaimRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BenefitResponseDto>> GetBenefitsByUserIdAsync(Guid userId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(userId);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuário com ID {userId} não encontrado.");
            }

            // 1. Obter todos os benefícios (Rewards)
            var allRewards = await _rewardRepository.GetAllAsync();

            // 2. Obter os benefícios já resgatados pelo usuário
            var claimedRewards = await _rewardClaimRepository.GetClaimsByUserIdAsync(userId);
            var claimedRewardIds = claimedRewards.Select(c => c.RewardId).ToHashSet();

            var responseList = new List<BenefitResponseDto>();

            foreach (var reward in allRewards)
            {
                var isClaimed = claimedRewardIds.Contains(reward.Id);
                var claim = claimedRewards.FirstOrDefault(c => c.RewardId == reward.Id);

                var dto = _mapper.Map<BenefitResponseDto>(reward);
                dto.IsClaimed = isClaimed;
                dto.ClaimedAt = claim?.ClaimedAt;

                responseList.Add(dto);
            }

            return responseList;
        }

        public async Task<BenefitResponseDto> ClaimRewardAsync(Guid userId, Guid rewardId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(userId);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuário com ID {userId} não encontrado.");
            }

            var reward = await _rewardRepository.GetByIdAsync(rewardId);
            if (reward == null)
            {
                throw new NotFoundException($"Benefício (Reward) com ID {rewardId} não encontrado.");
            }

            // Regra de Negócio 1: Pontuação Suficiente
            if (usuario.Pontos < reward.CostPoints)
            {
                throw new BusinessRuleException($"Pontuação insuficiente. Você precisa de {reward.CostPoints} pontos, mas tem apenas {usuario.Pontos}.");
            }

            // Regra de Negócio 2: Não pode resgatar o mesmo benefício mais de uma vez (simplificação)
            var existingClaim = await _rewardClaimRepository.GetClaimsByUserIdAsync(userId);
            if (existingClaim.Any(c => c.RewardId == rewardId))
            {
                throw new ConflictException("Você já resgatou este benefício.");
            }

            // 1. Criar o Resgate (Claim)
            var rewardClaim = new RewardClaim
            {
                UserId = userId,
                RewardId = rewardId,
                PointsSpent = reward.CostPoints,
                ClaimedAt = DateTime.UtcNow,
                Status = "CLAIMED"
            };

            await _rewardClaimRepository.AddAsync(rewardClaim);

            // 2. Atualizar a pontuação do usuário
            usuario.Pontos -= reward.CostPoints;
            _usuarioRepository.Update(usuario);

            await _rewardClaimRepository.SaveChangesAsync(); // Salva o Claim e a atualização do Usuário

            var responseDto = _mapper.Map<BenefitResponseDto>(reward);
            responseDto.IsClaimed = true;
            responseDto.ClaimedAt = rewardClaim.ClaimedAt;

            return responseDto;
        }
    }
}
