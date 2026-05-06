using CarePlusApi.DTOs;
using CarePlusApi.Exceptions;
using CarePlusApi.Interfaces;
using CarePlusApi.Models;

namespace CarePlusApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<UsuarioResponseDto> RegistrarAsync(UsuarioCreateDto dto)
        {
            // Verifica se o email já está em uso
            var existingUser = await _usuarioRepository.FindAsync(u => u.Email == dto.Email);
            if (existingUser.Any())
            {
                throw new ConflictException("Email já está em uso.");
            }

            var novoUsuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                // Fazendo o hash da senha
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _usuarioRepository.AddAsync(novoUsuario);
            await _usuarioRepository.SaveChangesAsync();

            var token = _tokenService.GenerateToken(novoUsuario);

            return new UsuarioResponseDto
            {
                Id = novoUsuario.Id,
                Nome = novoUsuario.Nome,
                Email = novoUsuario.Email,
                Pontos = novoUsuario.Pontos,
                StepsToday = novoUsuario.StepsToday,
                AvatarUrl = novoUsuario.AvatarUrl,
                Token = token
            };
        }

        public async Task<UsuarioResponseDto> LoginAsync(UsuarioLoginDto dto)
        {
            var usuarios = await _usuarioRepository.FindAsync(u => u.Email == dto.Email);
            var usuario = usuarios.FirstOrDefault();

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                throw new BusinessRuleException("Email ou senha inválidos.");
            }

            var token = _tokenService.GenerateToken(usuario);

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Pontos = usuario.Pontos,
                StepsToday = usuario.StepsToday,
                AvatarUrl = usuario.AvatarUrl,
                Token = token
            };
        }

        public async Task<UsuarioResponseDto> ObterPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Pontos = usuario.Pontos,
                StepsToday = usuario.StepsToday,
                AvatarUrl = usuario.AvatarUrl
            };
        }
    }
}
