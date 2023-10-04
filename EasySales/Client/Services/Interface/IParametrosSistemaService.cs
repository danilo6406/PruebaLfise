using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IParametrosSistemaService
    {
        Task<IEnumerable<ParametrosSistema>> Buscar(string? Filtro);
        Task<IEnumerable<ParametrosSistema>> CargarDatos();
        Task<ParametrosSistema> ObtenerXId(long id);
        Task<ParametrosSistema> ObtenerXNombre(string Nombre);
        Task<ParametrosSistema> Agregar(ParametrosSistema claseEntrante);
        Task<ParametrosSistema> Modificar(ParametrosSistema claseEntrante);
        Task Eliminar(long id);
    }
}