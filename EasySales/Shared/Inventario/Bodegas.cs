using System.ComponentModel.DataAnnotations;


namespace EasySales.Shared
{
    public class Bodegas
    {
        [Required]
        public int Id { get; set; }

        //public int EmpresaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

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
