using System.ComponentModel.DataAnnotations;

namespace CarePlusApi.DTOs
{
    // DTO para registrar o progresso do usuário em um desafio
    public class UpdateUserChallengeProgressDto
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "O progresso percentual é obrigatório.")]
        [Range(0, 100, ErrorMessage = "O progresso deve ser entre 0 e 100.")]
        public int ProgressValue { get; set; }

        public bool Completed { get; set; } = false;
    }

    // DTO de Resposta para o progresso do usuário
    public class UserChallengeResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChallengeId { get; set; }
        public string ChallengeTitulo { get; set; } = string.Empty;
        public int ProgressValue { get; set; }
        public bool Completed { get; set; }
        public int EarnedPoints { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
