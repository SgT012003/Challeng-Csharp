using System.ComponentModel.DataAnnotations;

namespace CarePlusApi.DTOs
{
    // DTO de Resposta (simplificado para o contexto atual)
    public class UsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PontuacaoTotal { get; set; }
    }
}
