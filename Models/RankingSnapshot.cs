using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("RankingSnapshots")]
    public class RankingSnapshot
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public DateOnly SnapshotDate { get; set; }

        // Propriedade de Navegação
        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; } = null!;
    }
}
