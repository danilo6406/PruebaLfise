using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class TasaCambioOficial
    {
        [Required]
        [Key]
        public long Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public int MonedaBaseId { get; set; }
        
        public int? MonedaCambioId { get; set; }

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
