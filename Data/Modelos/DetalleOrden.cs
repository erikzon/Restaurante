using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class DetalleOrden
    {
        [Key]
        public int DetalleOrdenId { get; set; }

        [Required(ErrorMessage = "La orden es obligatoria.")]
        public int OrdenId { get; set; }

        [ForeignKey("OrdenId")]
        public virtual Orden Orden { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Subtotal { get; set; }
    }
}
