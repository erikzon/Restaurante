using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Mesa
    {
        [Key]
        public int MesaId { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        public bool Ocupada { get; set; }

        public virtual ICollection<Orden> Ordenes { get; set; }
    }
}
