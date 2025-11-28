namespace CarePlusApi.DTOs
{
    // DTO de Resposta para o Ranking
    public class RankingResponseDto
    {
        public int Position { get; set; }
        public Guid UserId { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public int PontuacaoTotal { get; set; }
    }
}
