using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IClientesRepository
    {
        Task<IEnumerable<Clientes>> CargarDatos();
        Task<Clientes> ObtenerXId(long Id);
        Task<Clientes> ObtenerXNombre(string Nombre);
        Task<Clientes> ObtenerXIdentificacion(string Identificacion);
        Task<Clientes> Modificar(Clientes claseEntrante);
        Task<Clientes> Agregar(Clientes claseEntrante);
        Task Eliminar(long Id);
    }
}
