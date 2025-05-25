using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de la categoría no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
