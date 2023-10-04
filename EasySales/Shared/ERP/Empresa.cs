using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class Empresa
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string NombreComercial { get; set; }

        [Required]
        public string RazonSocial { get; set; }

        [Required]
        public string NumeroRuc { get; set; }

        [Required]
        public RegimenEmpresarial RegimenEmpresarial { get; set; }


        #region Propiedades Auditoria

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public TipoModificacion? TipoModificacion { get; set; }

        #endregion

    }
}
