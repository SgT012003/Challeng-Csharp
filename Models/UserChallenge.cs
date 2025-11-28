using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("UserChallenges")]
    public class UserChallenge
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ChallengeId { get; set; }

        [Required]
        [Range(0, 100)]
        public int ProgressValue { get; set; } = 0; // % 0 - 100

        [Required]
        public bool Completed { get; set; } = false;

        public DateTime? CompletedAt { get; set; }

        public int EarnedPoints { get; set; } = 0;

        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

        // Propriedades de Navegação
        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; } = null!;
    }
}
