using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IReservacionesRepository
    {
        Task<Reservaciones> Agregar(Reservaciones claseEntrante);
        Task Anular(long Id);
        Task<IEnumerable<Reservaciones>> CargarDatos();
        Task<Reservaciones> Modificar(Reservaciones claseEntrante);
        Task<Reservaciones> ObtenerXId(long Id);
        Task<long> ComprobarDisponibilidad(Reservaciones claseEntrante);
    }
}