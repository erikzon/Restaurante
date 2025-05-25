using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Mesa
    {
        [Key]
        public int MesaId { get; set; }

        [Required(ErrorMessage = "El número de mesa es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de mesa no puede tener más de 20 caracteres.")]
        public string Numero { get; set; }

        public bool Ocupada { get; set; }

        public virtual ICollection<Orden> Ordenes { get; set; }
    }
}
