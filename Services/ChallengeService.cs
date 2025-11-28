using CarePlusApi.DTOs;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;
using CarePlusApi.Exceptions;
using AutoMapper;

namespace CarePlusApi.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserChallengeRepository _userChallengeRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ChallengeService(IChallengeRepository challengeRepository, IUserChallengeRepository userChallengeRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _userChallengeRepository = userChallengeRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<ChallengeResponseDto> CreateChallengeAsync(CreateChallengeDto dto)
        {
            var challenge = _mapper.Map<Challenge>(dto);
            challenge.CreatedAt = DateTime.UtcNow;
            challenge.UpdatedAt = DateTime.UtcNow;

            await _challengeRepository.AddAsync(challenge);
            await _challengeRepository.SaveChangesAsync();

            return _mapper.Map<ChallengeResponseDto>(challenge);
        }

        public async Task<IEnumerable<ChallengeResponseDto>> GetChallengesAsync(bool includeDeleted = false)
        {
            IEnumerable<Challenge> challenges;
            if (includeDeleted)
            {
                challenges = await _challengeRepository.GetAllChallengesAsync();
            }
            else
            {
                challenges = await _challengeRepository.GetActiveChallengesAsync();
            }

            return _mapper.Map<IEnumerable<ChallengeResponseDto>>(challenges);
        }

        public async Task<ChallengeResponseDto> GetChallengeByIdAsync(Guid id)
        {
            var challenge = await _challengeRepository.GetByIdAsync(id);
            if (challenge == null)
            {
                throw new NotFoundException($"Desafio com ID {id} não encontrado.");
            }

            return _mapper.Map<ChallengeResponseDto>(challenge);
        }

        public async Task<ChallengeResponseDto> UpdateChallengeAsync(Guid id, UpdateChallengeDto dto)
        {
            var challenge = await _challengeRepository.GetByIdAsync(id);
            if (challenge == null)
            {
                throw new NotFoundException($"Desafio com ID {id} não encontrado.");
            }

            // Mapeamento parcial
            if (dto.Titulo != null) challenge.Titulo = dto.Titulo;
            if (dto.Descricao != null) challenge.Descricao = dto.Descricao;
            if (dto.Icon != null) challenge.Icon = dto.Icon;
            if (dto.RequiredValue.HasValue) challenge.RequiredValue = dto.RequiredValue.Value;
            if (dto.RewardPoints.HasValue) challenge.RewardPoints = dto.RewardPoints.Value;
            if (dto.Category != null) challenge.Category = dto.Category;
            if (dto.IsDaily.HasValue) challenge.IsDaily = dto.IsDaily.Value;
            if (dto.DataLimitePontuacao.HasValue) challenge.DataLimitePontuacao = dto.DataLimitePontuacao.Value;
            if (dto.Status.HasValue) challenge.Status = dto.Status.Value;

            challenge.UpdatedAt = DateTime.UtcNow;

            _challengeRepository.Update(challenge);
            await _challengeRepository.SaveChangesAsync();

            return _mapper.Map<ChallengeResponseDto>(challenge);
        }

        public async Task DeleteChallengeAsync(Guid id)
        {
            var challenge = await _challengeRepository.GetByIdAsync(id);
            if (challenge == null)
            {
                throw new NotFoundException($"Desafio com ID {id} não encontrado.");
            }

            // Exclusão lógica
            challenge.IsDeleted = true;
            challenge.Status = ChallengeStatus.Deleted;
            challenge.UpdatedAt = DateTime.UtcNow;

            _challengeRepository.Update(challenge);
            await _challengeRepository.SaveChangesAsync();
        }

        public async Task<UserChallengeResponseDto> UpdateUserChallengeProgressAsync(Guid challengeId, UpdateUserChallengeProgressDto dto)
        {
            var challenge = await _challengeRepository.GetByIdAsync(challengeId);
            if (challenge == null || challenge.Status != ChallengeStatus.Go)
            {
                throw new NotFoundException($"Desafio com ID {challengeId} não encontrado ou não está ativo.");
            }

            var usuario = await _usuarioRepository.GetByIdAsync(dto.UserId);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuário com ID {dto.UserId} não encontrado.");
            }

            var userChallenge = await _userChallengeRepository.GetByUserIdAndChallengeIdAsync(dto.UserId, challengeId);

            if (userChallenge == null)
            {
                // Cria um novo UserChallenge se não existir
                userChallenge = new UserChallenge
                {
                    UserId = dto.UserId,
                    ChallengeId = challengeId,
                    ProgressValue = dto.ProgressValue,
                    Completed = dto.Completed,
                    LastUpdate = DateTime.UtcNow
                };
                await _userChallengeRepository.AddAsync(userChallenge);
            }
            else
            {
                // Atualiza o UserChallenge existente
                if (userChallenge.Completed)
                {
                    throw new ConflictException("Este desafio já foi completado pelo usuário.");
                }

                userChallenge.ProgressValue = dto.ProgressValue;
                userChallenge.Completed = dto.Completed;
                userChallenge.LastUpdate = DateTime.UtcNow;

                _userChallengeRepository.Update(userChallenge);
            }

            // Lógica de Pontuação
            if (userChallenge.Completed && userChallenge.EarnedPoints == 0)
            {
                // Verifica se a pontuação ainda é válida
                if (challenge.DataLimitePontuacao >= DateTime.UtcNow)
                {
                    userChallenge.EarnedPoints = challenge.RewardPoints;
                    userChallenge.CompletedAt = DateTime.UtcNow;

                    // Atualiza a pontuação total do usuário
                    usuario.Pontos += challenge.RewardPoints;
                    _usuarioRepository.Update(usuario);
                }
                else
                {
                    // Desafio completo, mas fora do prazo de pontuação
                    userChallenge.CompletedAt = DateTime.UtcNow;
                    userChallenge.EarnedPoints = 0;
                }
            }

            await _userChallengeRepository.SaveChangesAsync();

            return _mapper.Map<UserChallengeResponseDto>(userChallenge);
        }
    }
}
