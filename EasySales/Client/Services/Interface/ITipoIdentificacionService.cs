using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface ITipoIdentificacionService
    {
        Task<IEnumerable<TipoIdentificacion>> Buscar(string? Filtro);
        Task<IEnumerable<TipoIdentificacion>> CargarDatos();
        Task<TipoIdentificacion> ObtenerXId(long id);
        Task<TipoIdentificacion> ObtenerXNombre(string Nombre);
        Task<TipoIdentificacion> Agregar(TipoIdentificacion claseEntrante);
        Task<TipoIdentificacion> Modificar(TipoIdentificacion claseEntrante);
        Task Eliminar(long id);
    }
}
