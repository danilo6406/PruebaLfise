using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace EasySales.Server.Models
{
    public class VentasRepository : IVentasRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public VentasRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<Facturas> Agregar(Facturas claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacion = tipoModificacion;
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                var cliente = await appDbContext.Clientes.FirstOrDefaultAsync(e => e.Id == claseEntrante.ClienteId);
                claseEntrante.Cliente = cliente;
                var resultado = await appDbContext.Facturas.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task AgregarFacturaDetalle(FacturaDetalle claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                var resultado = await appDbContext.FacturaDetalle.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();

                //Restar cantidad en inventario
                CantidadProductoInventario cantidadProductoInventario = new()
                {
                    ProductoId = claseEntrante.ProductoId,
                    EmpresaId = claseEntrante.EmpresaId,
                    BodegaId = 1,
                    Cantidad = claseEntrante.Cantidad
                };
                await RestarProductoInventario(cantidadProductoInventario);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Facturas> ObtenerXId(long Id)
        {
            try
            {
                return await appDbContext.Facturas.FirstOrDefaultAsync(e => e.Id == Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FacturaDetalle>> ObtenerFacturaDetalleXId(long Id)
        {
            try
            {
                var facturaDetalles = await appDbContext.FacturaDetalle.Include(e => e.Producto).Where(e => e.FacturaId == Id).ToListAsync();
                return facturaDetalles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task RestarProductoInventario(CantidadProductoInventario cantidadProductoInventario)
        {
            try
            {
                string sp = ProcedimientosAlmacenadosSql.Instance.SPRestarProductoInventario;
                using (MySqlConnection connection = new(configuration.GetConnectionString("DBConnection")))
                {
                    using (MySqlCommand command = new(sp, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter to the command
                        command.Parameters.AddWithValue("_EmpresaId", cantidadProductoInventario.EmpresaId);
                        command.Parameters.AddWithValue("_ProductoId", cantidadProductoInventario.ProductoId);
                        command.Parameters.AddWithValue("_BodegaId", cantidadProductoInventario.BodegaId);
                        command.Parameters.AddWithValue("_CantidadMenos", cantidadProductoInventario.Cantidad);
                       
                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
