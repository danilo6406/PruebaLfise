using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IParametrosSistemaRepository
    {
        Task<IEnumerable<ParametrosSistema>> CargarDatos();
        Task<ParametrosSistema> ObtenerXId(long Id);
        Task<ParametrosSistema> ObtenerXNombre(string Nombre);
        Task<ParametrosSistema> Modificar(ParametrosSistema claseEntrante);
        Task<ParametrosSistema> Agregar(ParametrosSistema claseEntrante);
        Task Eliminar(long Id);
        Task<string> ObtenerValorStringXNombre(string Nombre);
        Task<decimal> ObtenerValorNumericoXNombre(string Nombre);
    }
}
