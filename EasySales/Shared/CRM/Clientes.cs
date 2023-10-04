using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class Clientes
    {

        [Required]
        public long Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }

        [Required]
        public int TipoIdentificacionId { get; set; }

        public TipoIdentificacion? TipoIdentificacion { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "El Numero de teléfono no puede exceder los {1} caracteres. ")]
        [MinLength(8, ErrorMessage = "El Numero de teléfono no puede tener menos de los {1} caracteres. ")]
        public string NumeroTelefono { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public bool Activo { get; set; }

        #region Propiedades Auditoria

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public TipoModificacion? TipoModificacion { get; set; }

        public int? TipoModificacionId { get; set; }

        #endregion
    }
}
