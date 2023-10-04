using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IUsersService
    {
        Task<EasySalesServerUser> Agregar(EasySalesServerUser claseEntrante);
        Task<IEnumerable<EasySalesServerUser>> Buscar(string? Filtro);
        Task<IEnumerable<EasySalesServerUser>> CargarDatos();
        Task Eliminar(string id);
        Task<EasySalesServerUser> Modificar(EasySalesServerUser claseEntrante);
        Task<EasySalesServerUser> ObtenerXId(string id);
        Task<EasySalesServerUser> ObtenerXIdentificacion(string Identificacion);
        Task<EasySalesServerUser> ObtenerXNombre(string Nombre);
    }
}