using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("StepLogs")]
    public class StepLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateOnly LogDate { get; set; }

        [Required]
        public int Steps { get; set; }

        [StringLength(50)]
        public string SyncedFrom { get; set; } = string.Empty; // Ex: Wearable, Manual

        // Propriedade de Navegação
        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; } = null!;
    }
}
