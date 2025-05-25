using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(typeof(decimal), "0.01", "99999.99", ErrorMessage = "El precio debe ser un número decimal válido y mayor que cero.")]
        [DataType(DataType.Currency, ErrorMessage = "El precio debe ser un número válido.")]
        public decimal Precio { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El codigo es obligatorio.")]
        public string Codigo { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }
    }
}
