using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySales.Shared
{
    public class FacturaDetalle
    {
        [Required]
        [Key]
        public long Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public long FacturaId { get; set; }

    
        public Productos Producto { get; set; }

        [Required]
        public long ProductoId { get; set; }
        
        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        public decimal? Impuestos { get; set; }

        public decimal? DescuentoValor { get; set; }

        public int? PromocionId { get; set; }

        public int EstadoFacturaId { get; set; }

        #region Propiedades Auditoria

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public int TipoModificacionId { get; set; }

        #endregion

    }
}
