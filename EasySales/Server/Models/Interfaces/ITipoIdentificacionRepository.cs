using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface ITipoIdentificacionRepository
    {
        Task<IEnumerable<TipoIdentificacion>> CargarDatos();
        Task<TipoIdentificacion> ObtenerXId(long Id);
        Task<TipoIdentificacion> ObtenerXNombre(string Nombre);
        Task<TipoIdentificacion> Modificar(TipoIdentificacion claseEntrante);
        Task<TipoIdentificacion> Agregar(TipoIdentificacion claseEntrante);
        Task Eliminar(long Id);
    }
}
