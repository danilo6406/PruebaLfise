using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared
{
    public class Productos
    {
        [Required]
        public long Id { get; set; }

        public Empresa? Empresa { get; set; }

        public int EmpresaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal PrecioVenta { get; set; }

        public decimal PrecioCosto { get; set; }

        [Required]
        [Display(Name ="Es Servicio")]
        public bool EsServicio { get; set; }

        [Required]
        public bool? AplicaIVA { get; set; }

        [Required]
        public bool Activo { get; set; }


        public CategoriaProductos? CategoriaProductos { get; set; }

        public long? CategoriaProductosId { get; set; }

        public SubCategoriaProductos? SubCategoriaProductos { get; set; }

        public long? SubCategoriaProductosId { get; set; }

        public string? FotoPath { get; set; }

        //[NotMapped]
        //[Display(Name = "Nueva foto del producto")]
        //public IFormFile? FotoSrvLocal { get; set; }

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
