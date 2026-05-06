using CarePlusApi.DTOs;
using CarePlusApi.Exceptions;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;
using CarePlusApi.Services;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace CarePlusApi.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _usuarioService = new UsuarioService(_usuarioRepoMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task RegistrarAsync_ComEmailExistente_DeveLancarConflictException()
        {
            // Arrange
            var dto = new UsuarioCreateDto { Nome = "Teste", Email = "teste@teste.com", Password = "123" };
            _usuarioRepoMock.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(new List<Usuario> { new Usuario() });

            // Act
            Func<Task> action = async () => await _usuarioService.RegistrarAsync(dto);

            // Assert
            await action.Should().ThrowAsync<ConflictException>().WithMessage("Email já está em uso.");
        }

        [Fact]
        public async Task LoginAsync_ComCredenciaisInvalidas_DeveLancarBusinessRuleException()
        {
            // Arrange
            var dto = new UsuarioLoginDto { Email = "teste@teste.com", Password = "errada" };
            var usuario = new Usuario
            {
                Email = "teste@teste.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correta")
            };

            _usuarioRepoMock.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(new List<Usuario> { usuario });

            // Act
            Func<Task> action = async () => await _usuarioService.LoginAsync(dto);

            // Assert
            await action.Should().ThrowAsync<BusinessRuleException>().WithMessage("Email ou senha inválidos.");
        }

        [Fact]
        public async Task LoginAsync_ComCredenciaisValidas_DeveRetornarToken()
        {
            // Arrange
            var dto = new UsuarioLoginDto { Email = "teste@teste.com", Password = "correta" };
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Email = "teste@teste.com",
                Nome = "Teste",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correta")
            };

            _usuarioRepoMock.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                .ReturnsAsync(new List<Usuario> { usuario });
            
            _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<Usuario>())).Returns("token_falso");

            // Act
            var result = await _usuarioService.LoginAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().Be("token_falso");
            result.Email.Should().Be(dto.Email);
        }
    }
}
