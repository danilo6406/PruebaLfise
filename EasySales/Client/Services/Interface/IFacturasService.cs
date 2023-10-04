using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface IFacturasService
    {
        Task<Facturas> Anular(Facturas claseEntrante);
        Task<IEnumerable<Facturas>> CargarDatos();
        Task<IEnumerable<FacturaDetalle>> CargarDetalleFactura(long FacturaId);
        Task Eliminar(int id);
        Task<Facturas> Modificar(Facturas claseEntrante);
        Task<Facturas> ObtenerXId(long id);
    }
}