using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class TasaCambioCompra
    {
        [Required]
        [Key]
        public long Id { get; set; }

        public int EmpresaId { get; set; }

        [Required]
        public long TasaCambioOficialId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public int MonedaBaseId { get; set; }

        [Required]
        public int MonedaCambioId { get; set; }

        [Required]
        public bool Activo { get; set; }

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
