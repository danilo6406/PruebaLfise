using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IRolesRepository
    {
        Task<EasySalesServerRoles> Agregar(EasySalesServerRoles claseEntrante);
        Task<IEnumerable<EasySalesServerRoles>> CargarDatos();
        Task Eliminar(string Id);
        Task<EasySalesServerRoles> Modificar(EasySalesServerRoles claseEntrante);
        Task<EasySalesServerRoles> ObtenerXCodigo(string codigo);
        Task<EasySalesServerRoles> ObtenerXId(string Id);
        Task<EasySalesServerRoles> ObtenerXNombre(string Nombre);
        Task<IEnumerable<EasySalesServerRoles>> ObtenerXUsuario(string Id);
        Task EliminarRolUsuario(string IdRol, string IdUser);
        Task<string> AgregarRolUsuario(string IdRol, string IdUser);
    }
}