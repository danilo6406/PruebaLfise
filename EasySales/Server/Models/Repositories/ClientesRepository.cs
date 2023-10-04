using EasySales.Server.Data;
using EasySales.Server.Models.Interfaces;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models.Repositories
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly AppDbContext appDbContext;

        public ClientesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Clientes> Agregar(Clientes claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacion = tipoModificacion;
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                var resultado = await appDbContext.Clientes.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Clientes>> CargarDatos()
        {
            return await appDbContext.Clientes.Include(e => e.TipoIdentificacion).ToListAsync();
        }

        public async Task Eliminar(long Id)
        {
            var result = await appDbContext.Clientes
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.Clientes.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Clientes> Modificar(Clientes claseEntrante)
        {
            try
            {
                var result = await appDbContext.Clientes.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.Nombre = claseEntrante.Nombre;
                    result.NumeroIdentificacion = claseEntrante.NumeroIdentificacion;
                    result.TipoIdentificacionId = claseEntrante.TipoIdentificacionId;
                    result.Activo = claseEntrante.Activo;
                    result.FechaCreacion = claseEntrante.FechaCreacion;
                    result.UsuarioCreacion = claseEntrante.UsuarioCreacion;
                    result.FechaModificacion = DateTime.Now;
                    result.UsuarioModificacion = claseEntrante.UsuarioModificacion;
                    result.Id = claseEntrante.Id;
                    result.TipoModificacion = tipoModificacion;
                    result.TipoModificacionId = tipoModificacion.Id;
                    result.NumeroTelefono = claseEntrante.NumeroTelefono;
                    result.Email = claseEntrante.Email;

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

        public async Task<Clientes> ObtenerXId(long Id)
        {
            return await appDbContext.Clientes.Include(i => i.TipoIdentificacion).FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<Clientes> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.Clientes.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }

        public async Task<Clientes> ObtenerXIdentificacion(string Identificacion)
        {
            return await appDbContext.Clientes.FirstOrDefaultAsync(e => e.NumeroIdentificacion == Identificacion);
        }

    }
}
