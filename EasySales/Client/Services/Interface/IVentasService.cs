using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IVentasService
    {
        Task<Facturas> Agregar(Dictionary<string, object> dictionary);

        Task<Facturas> ObtenerXId(long id);

        Task<IEnumerable<FacturaDetalle>> ObtenerFacturaDetalleXId(long id);
    }
}
