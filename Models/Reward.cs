using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("Rewards")]
    public class Reward
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int CostPoints { get; set; } // Pontuação necessária para desbloquear/resgatar

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [StringLength(50)]
        public string Category { get; set; } = string.Empty; // Ex: Desconto, Serviço Extra

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relação com resgates
        public ICollection<RewardClaim> RewardClaims { get; set; } = new List<RewardClaim>();
    }
}
