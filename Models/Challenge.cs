using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    public enum ChallengeStatus
    {
        Waiting = 0,
        Go = 1,
        Ended = 2,
        Deleted = 3
    }

    [Table("Challenges")]
    public class Challenge
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [StringLength(255)]
        public string? Icon { get; set; }

        [Required]
        public int RequiredValue { get; set; } // Valor necessário para completar (ex: 10000 passos)

        [Required]
        public int RewardPoints { get; set; } // Pontos que vale

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty; // Ex: CARDIO, FORCA, MENTE

        public bool IsDaily { get; set; } = false;

        [Required]
        public DateTime DataLimitePontuacao { get; set; } // Pontuação tem tempo para vencer

        [Required]
        public ChallengeStatus Status { get; set; } = ChallengeStatus.Waiting;

        public bool IsDeleted { get; set; } = false; // Exclusão lógica

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relação com o progresso dos usuários
        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
    }
}
