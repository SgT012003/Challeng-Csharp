using AutoMapper;
using CarePlusApi.DTOs;
using CarePlusApi.Exceptions;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;
using CarePlusApi.Services;
using FluentAssertions;
using Moq;

namespace CarePlusApi.Tests
{
    public class ChallengeServiceTests
    {
        private readonly Mock<IChallengeRepository> _challengeRepoMock;
        private readonly Mock<IUserChallengeRepository> _userChallengeRepoMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ChallengeService _challengeService;

        public ChallengeServiceTests()
        {
            _challengeRepoMock = new Mock<IChallengeRepository>();
            _userChallengeRepoMock = new Mock<IUserChallengeRepository>();
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();

            _challengeService = new ChallengeService(
                _challengeRepoMock.Object,
                _userChallengeRepoMock.Object,
                _usuarioRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetChallengeByIdAsync_ChallengeExiste_DeveRetornarChallengeResponseDto()
        {
            // Arrange
            var challengeId = Guid.NewGuid();
            var challenge = new Challenge { Id = challengeId, Titulo = "Desafio 1" };
            var responseDto = new ChallengeResponseDto { Id = challengeId, Titulo = "Desafio 1" };

            _challengeRepoMock.Setup(repo => repo.GetByIdAsync(challengeId)).ReturnsAsync(challenge);
            _mapperMock.Setup(m => m.Map<ChallengeResponseDto>(challenge)).Returns(responseDto);

            // Act
            var result = await _challengeService.GetChallengeByIdAsync(challengeId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(challengeId);
            result.Titulo.Should().Be("Desafio 1");
        }

        [Fact]
        public async Task GetChallengeByIdAsync_ChallengeNaoExiste_DeveLancarNotFoundException()
        {
            // Arrange
            var challengeId = Guid.NewGuid();
            _challengeRepoMock.Setup(repo => repo.GetByIdAsync(challengeId)).ReturnsAsync((Challenge?)null);

            // Act
            Func<Task> action = async () => await _challengeService.GetChallengeByIdAsync(challengeId);

            // Assert
            await action.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Desafio com ID {challengeId} não encontrado.");
        }

        [Fact]
        public async Task UpdateUserChallengeProgressAsync_ChallengeNaoAtivo_DeveLancarNotFoundException()
        {
            // Arrange
            var challengeId = Guid.NewGuid();
            var dto = new UpdateUserChallengeProgressDto { UserId = Guid.NewGuid(), ProgressValue = 10 };
            var challenge = new Challenge { Id = challengeId, Status = ChallengeStatus.Waiting }; // Não é Go

            _challengeRepoMock.Setup(repo => repo.GetByIdAsync(challengeId)).ReturnsAsync(challenge);

            // Act
            Func<Task> action = async () => await _challengeService.UpdateUserChallengeProgressAsync(challengeId, dto);

            // Assert
            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}
