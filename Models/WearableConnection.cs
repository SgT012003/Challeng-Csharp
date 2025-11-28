using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("WearableConnections")]
    public class WearableConnection
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Provider { get; set; } = string.Empty; // Ex: Google Fit, Apple Health

        [Required]
        public string AccessToken { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime LastSyncedAt { get; set; } = DateTime.UtcNow;

        // Propriedade de Navegação
        [ForeignKey("UserId")]
        public Usuario Usuario { get; set; } = null!;
    }
}
