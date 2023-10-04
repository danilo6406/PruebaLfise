using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared
{
    public class SubCategoriaProductos
    {
        [Required]
        public long Id { get; set; }

        public int? EmpresaId { get; set; }

        
        public CategoriaProductos? CategoriaProductos { get; set; }

 
        public long? CategoriaProductosId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

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
