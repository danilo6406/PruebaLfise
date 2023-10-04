using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class Reservaciones
    {
        
        public long Id { get; set; }

        public int? EmpresaId { get; set; }

        public Clientes? Cliente { get; set; }

        public long ClienteId { get; set; }

        public Productos? Producto { get; set; }

        public long ProductoId { get; set; }

        [Required(ErrorMessage = "El campo Fecha Reservación es requerido.")]
        public DateTime FechaReservacion { get; set; }

        [Required(ErrorMessage = "El campo Monto Reservación es requerido.")]
        public decimal MontoReservacion { get; set; }

        public int? CantidadHoras { get; set; }

        [Required(ErrorMessage = "El campo Hora Inicio es requerido.")]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "El campo Hora Fin es requerido.")]
        public DateTime HoraFin { get; set; }

        public EstadoReserva? EstadoReserva { get; set; }

        public int? EstadoReservaId { get; set; }

        public string? Observacion { get; set; }

        [StringLength(50, ErrorMessage = "El titulo del evento no puede exceder los {1} caracteres. ")]
        public string Titulo { get; set; }

        #region Propiedades Auditoria

        public string UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public TipoModificacion? TipoModificacion { get; set; }

        public int? TipoModificacionId { get; set; }

        #endregion

    }
}
