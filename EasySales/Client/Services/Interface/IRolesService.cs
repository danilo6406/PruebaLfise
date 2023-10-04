using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IRolesService
    {
        Task<EasySalesServerRoles> Agregar(EasySalesServerRoles claseEntrante);
        Task<IEnumerable<EasySalesServerRoles>> Buscar(string? Filtro);
        Task<IEnumerable<EasySalesServerRoles>> CargarDatos();
        Task Eliminar(string id);
        Task<EasySalesServerRoles> Modificar(EasySalesServerRoles claseEntrante);
        Task<EasySalesServerRoles> ObtenerXCodigo(string codigo);
        Task<EasySalesServerRoles> ObtenerXId(long id);
        Task<EasySalesServerRoles> ObtenerXNombre(string Nombre);
        Task<IEnumerable<EasySalesServerRoles>> ObtenerXUsuario(string id);
        Task EliminarRolUsuario(string IdRol, string IdUser);
        Task<string> AgregarRolUsuario(string IdRol, string IdUser);
    }
}