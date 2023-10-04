using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface IFacturasRepository
    {
        Task<Facturas> Agregar(Facturas claseEntrante);
        Task<IEnumerable<Facturas>> CargarDatos();
        Task<IEnumerable<FacturaDetalle>> CargarDetalleFactura(long Id);
        Task Eliminar(long Id);
        Task<Facturas> Modificar(Facturas claseEntrante);        
        Task<Facturas> Anular(Facturas claseEntrante);
        Task<IEnumerable<Facturas>> ObtenerXClienteId(int clienteId);
        Task<Facturas> ObtenerXId(long Id);
    }
}