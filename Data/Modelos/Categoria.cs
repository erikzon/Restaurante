using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
