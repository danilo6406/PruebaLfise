using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class Cajas
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        public int? ScurusalId { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public bool Activo { get; set; }

        public bool Abierta { get; set; }

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
