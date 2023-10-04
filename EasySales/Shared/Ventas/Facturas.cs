using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class Facturas
    {
        [Required]
        public long Id { get; set; }

        public int EmpresaID { get; set; }

        public int? CajaId { get; set; }

        public Empresa? Empresa { get; set; }

        public int SucursalId { get; set; }

        public Sucursales? Sucursal { get; set; }

        public string? NumeroFactura { get; set; }

        [Required]        
        public DateTime Fecha { get; set; }

        public int EstadoFacturaId { get; set; }

        public EstadosFactura? EstadoFactura { get; set; }

        public long ClienteId { get; set; }

        public Clientes? Cliente { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        public decimal Impuestos { get; set; }

        public decimal? DescuentoValor { get; set; }

        public int? PromocionId { get; set; }

        [Required]
        public decimal Total { get; set; }

        #region Propiedades Auditoria

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public int TipoModificacionId { get; set; }

        public TipoModificacion? TipoModificacion { get; set; }

        #endregion

    }
}
