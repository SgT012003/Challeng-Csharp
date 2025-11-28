using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarePlusApi.Models
{
    [Table("Beneficios")]
    public class Benefit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Required]
        public int PontuacaoMinima { get; set; } // Pontuação necessária para desbloquear
    }
}
