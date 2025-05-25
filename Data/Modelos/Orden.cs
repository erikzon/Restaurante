using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }

        [Required(ErrorMessage = "La mesa es obligatoria.")]
        public int MesaId { get; set; }

        [ForeignKey("MesaId")]
        public virtual Mesa Mesa { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaCierre { get; set; }

        [Required(ErrorMessage = "El estado de la orden es obligatorio.")]
        public EstadoOrden Estado { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo o cero.")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, double.MaxValue, ErrorMessage = "La propina no puede ser negativa.")]
        public decimal Propina { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, double.MaxValue, ErrorMessage = "El total no puede ser negativo o cero.")]
        public decimal Total { get; set; }

        public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; }
    }

    public enum EstadoOrden
    {
        Activa,
        Cancelada,
        Pagada
    }
}
