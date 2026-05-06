using CarePlusApi.DTOs;
using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> RegistrarAsync(UsuarioCreateDto dto);
        Task<UsuarioResponseDto> LoginAsync(UsuarioLoginDto dto);
        Task<UsuarioResponseDto> ObterPorIdAsync(Guid id);
    }
}
