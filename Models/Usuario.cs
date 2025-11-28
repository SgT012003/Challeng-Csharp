using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(255)]
        public string? AvatarUrl { get; set; }

        // Pontuação total do usuário (para o ranking)
        public int Pontos { get; set; } = 0;

        public int StepsToday { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relações
        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
        public WearableConnection? WearableConnection { get; set; }
        public ICollection<StepLog> StepLogs { get; set; } = new List<StepLog>();
        public ICollection<RewardClaim> RewardClaims { get; set; } = new List<RewardClaim>();
        public ICollection<RankingSnapshot> RankingSnapshots { get; set; } = new List<RankingSnapshot>();
    }
}
