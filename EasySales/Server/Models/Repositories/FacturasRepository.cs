using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class FacturasRepository : IFacturasRepository
    {

        private readonly AppDbContext appDbContext;

        public FacturasRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Facturas> Agregar(Facturas claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                claseEntrante.FechaCreacion = DateTime.Now;
                var resultado = await appDbContext.Facturas.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Facturas>> CargarDatos()
        {
            try
            {
                return await appDbContext.Facturas.Include(e => e.Cliente).Include(e => e.EstadoFactura).OrderByDescending(e=>e.Id).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FacturaDetalle>> CargarDetalleFactura(long Id)
        {
            try
            {
                return await appDbContext.FacturaDetalle.Include(e => e.Producto).Where(e => e.FacturaId == Id).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Eliminar(long Id)
        {
            var result = await appDbContext.Facturas
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.Facturas.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Facturas> Modificar(Facturas claseEntrante)
        {
            try
            {
                var result = await appDbContext.Facturas.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.EstadoFacturaId = claseEntrante.EstadoFacturaId;
                    result.ClienteId = claseEntrante.ClienteId;
                    result.PromocionId = claseEntrante.PromocionId;
                    result.FechaCreacion = claseEntrante.FechaCreacion;
                    result.UsuarioCreacion = claseEntrante.UsuarioCreacion;
                    result.FechaModificacion = DateTime.Now;
                    result.UsuarioModificacion = claseEntrante.UsuarioModificacion;
                    result.TipoModificacionId = tipoModificacion.Id;

                    await appDbContext.SaveChangesAsync();

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public async Task<Facturas> Anular(Facturas claseEntrante)
        {
            try
            {
                var result = await appDbContext.Facturas.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");
                var estadoFactura = await appDbContext.EstadosFactura.FirstOrDefaultAsync(e => e.Nombre == "Anulada");

                if (result != null)
                {
                    result.EstadoFacturaId = estadoFactura.Id;
                    result.ClienteId = claseEntrante.ClienteId;
                    result.PromocionId = claseEntrante.PromocionId;
                    result.FechaCreacion = claseEntrante.FechaCreacion;
                    result.UsuarioCreacion = claseEntrante.UsuarioCreacion;
                    result.FechaModificacion = DateTime.Now;
                    result.UsuarioModificacion = claseEntrante.UsuarioModificacion;
                    result.TipoModificacionId = tipoModificacion.Id;

                    await appDbContext.SaveChangesAsync();

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public async Task<Facturas> ObtenerXId(long Id)
        {
            try
            {
                return await appDbContext.Facturas.Include(e => e.Cliente).Include(e => e.EstadoFactura).FirstOrDefaultAsync(e => e.Id == Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Facturas>> ObtenerXClienteId(int clienteId)
        {
            try
            {
                return await appDbContext.Facturas.Where(e => e.ClienteId == clienteId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
