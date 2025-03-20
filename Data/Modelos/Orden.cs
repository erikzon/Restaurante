using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.Modelos
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }

        public int MesaId { get; set; }

        [ForeignKey("MesaId")]
        public virtual Mesa Mesa { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaCierre { get; set; }

        [Required]
        public EstadoOrden Estado { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal IVA { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
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
