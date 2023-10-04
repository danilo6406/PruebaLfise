using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IClientesServices
    {
        Task<IEnumerable<Clientes>> Buscar(string? Filtro);
        Task<IEnumerable<Clientes>> CargarDatos();
        Task<Clientes> ObtenerXId(long id);
        Task<Clientes> ObtenerXIdentificacion(string Identificacion);
        Task<Clientes> ObtenerXNombre(string Nombre);
        Task<Clientes> Agregar(Clientes claseEntrante);
        Task<Clientes> Modificar(Clientes claseEntrante);
        Task Eliminar(long id);
    }
}
