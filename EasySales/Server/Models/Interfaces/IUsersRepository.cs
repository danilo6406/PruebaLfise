using EasySales.Shared;

namespace EasySales.Server.Models.Interfaces
{
    public interface IUsersRepository
    {
        Task<EasySalesServerUser> Agregar(EasySalesServerUser claseEntrante);
        Task<IEnumerable<EasySalesServerUser>> CargarDatos();
        Task Eliminar(string Id);
        Task<EasySalesServerUser> Modificar(EasySalesServerUser claseEntrante);
        Task<EasySalesServerUser> ObtenerXId(string Id);
        Task<EasySalesServerUser> ObtenerXIdentificacion(string Identificacion);
        Task<EasySalesServerUser> ObtenerXNombre(string Nombre);
    }
}