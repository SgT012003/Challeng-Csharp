using System.ComponentModel.DataAnnotations;
using CarePlusApi.Models;

namespace CarePlusApi.DTOs
{
    // DTO de Criação/Requisição
    public class CreateChallengeDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 e 150 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [StringLength(255)]
        public string? Icon { get; set; }

        [Required(ErrorMessage = "O valor requerido é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor requerido deve ser positivo.")]
        public int RequiredValue { get; set; }

        [Required(ErrorMessage = "A pontuação de recompensa é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A pontuação deve ser positiva.")]
        public int RewardPoints { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        public bool IsDaily { get; set; } = false;

        [Required(ErrorMessage = "A data limite para pontuação é obrigatória.")]
        public DateTime DataLimitePontuacao { get; set; }

        [Required(ErrorMessage = "O status inicial é obrigatório.")]
        public ChallengeStatus Status { get; set; } = ChallengeStatus.Waiting;
    }

    // DTO de Atualização
    public class UpdateChallengeDto
    {
        [StringLength(150, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 e 150 caracteres.")]
        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        [StringLength(255)]
        public string? Icon { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O valor requerido deve ser positivo.")]
        public int? RequiredValue { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A pontuação deve ser positiva.")]
        public int? RewardPoints { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        public bool? IsDaily { get; set; }

        public DateTime? DataLimitePontuacao { get; set; }

        public ChallengeStatus? Status { get; set; }
    }

    // DTO de Resposta
    public class ChallengeResponseDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Icon { get; set; }
        public int RequiredValue { get; set; }
        public int RewardPoints { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsDaily { get; set; }
        public DateTime DataLimitePontuacao { get; set; }
        public ChallengeStatus Status { get; set; }
    }
}
