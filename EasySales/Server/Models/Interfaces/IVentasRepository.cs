using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IVentasRepository
    {
        Task<Facturas> ObtenerXId(long Id);

        Task<Facturas> Agregar(Facturas claseEntrante);

        Task AgregarFacturaDetalle(FacturaDetalle claseEntrante);

        Task<IEnumerable<FacturaDetalle>> ObtenerFacturaDetalleXId(long Id);
    }
}
