using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IBodegaService
    {
        Task<Bodegas> Agregar(Bodegas claseEntrante);
        Task<IEnumerable<Bodegas>> Buscar(string? Filtro);
        Task<IEnumerable<Bodegas>> CargarDatos();
        Task Eliminar(int id);
        Task<Bodegas> Modificar(Bodegas claseEntrante);
        Task<TipoIdentificacion> ObtenerXId(long id);
        Task<TipoIdentificacion> ObtenerXNombre(string Nombre);
    }
}