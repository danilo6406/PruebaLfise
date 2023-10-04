using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared
{
    public class ParametrosSistema
    {
        [Required]
        public long Id { get; set; }

        public int? EmpresaId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nombre tiene que tener 50 caracteres maximo.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Descripcion tiene que tener 200 caracteres maximo.")]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public decimal? ValorNumerico { get; set; }

        [StringLength(500, ErrorMessage = "Valor tiene que tener 500 caracteres maximo.")]
        public string? ValorString { get; set; }

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
