using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IBodegasRepository
    {
        Task<Bodegas> Agregar(Bodegas claseEntrante);
        Task<IEnumerable<Bodegas>> CargarDatos();
        Task Eliminar(int Id);
        Task<Bodegas> Modificar(Bodegas claseEntrante);
        Task<Bodegas> ObtenerXId(int Id);
        Task<Bodegas> ObtenerXNombre(string Nombre);
    }
}