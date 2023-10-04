using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IReservacionesService
    {
        Task<Reservaciones> Agregar(Reservaciones claseEntrante);
        Task Anular(long Id);
        Task<IEnumerable<Reservaciones>> CargarDatos();
        Task<Reservaciones> Modificar(Reservaciones claseEntrante);
        Task<Reservaciones> ObtenerXId(long Id);
        Task<Reservaciones> VerificarDisponibilidad(Reservaciones claseEntrante);

    }
}