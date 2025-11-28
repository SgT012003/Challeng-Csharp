using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("RewardClaims")]
    public class RewardClaim
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid RewardId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int PointsSpent { get; set; }

        public DateTime ClaimedAt { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string Status { get; set; } = "CLAIMED"; // Ex: CLAIMED, USED, CANCELLED

        // Propriedades de Navegação
        [ForeignKey("RewardId")]
        public Reward Reward { get; set; } = null!;

        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; } = null!;
    }
}
