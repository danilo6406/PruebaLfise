using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class Moneda
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(2)]
        public string CodigoIsoAlpha2Code { get; set; }

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
