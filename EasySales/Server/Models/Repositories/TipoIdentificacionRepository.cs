using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class TipoIdentificacionRepository : ITipoIdentificacionRepository
    {
        private readonly AppDbContext appDbContext;

        public TipoIdentificacionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<TipoIdentificacion> Agregar(TipoIdentificacion claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacion = tipoModificacion;
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                var resultado = await appDbContext.TipoIdentificacion.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        public async Task<IEnumerable<TipoIdentificacion>> CargarDatos()
        {
            return await appDbContext.TipoIdentificacion.ToListAsync();
        }

        public async Task Eliminar(long Id)
        {
            var result = await appDbContext.TipoIdentificacion
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.TipoIdentificacion.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<TipoIdentificacion> Modificar(TipoIdentificacion claseEntrante)
        {
            try
            {
                var result = await appDbContext.TipoIdentificacion.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.Nombre = claseEntrante.Nombre;
                    result.Descripcion = claseEntrante.Descripcion;
                    result.Activo = claseEntrante.Activo;
                    result.FechaCreacion = claseEntrante.FechaCreacion;
                    result.UsuarioCreacion = claseEntrante.UsuarioCreacion;
                    result.FechaModificacion = DateTime.Now;
                    result.UsuarioModificacion = claseEntrante.UsuarioModificacion;
                    result.Id = claseEntrante.Id;
                    result.TipoModificacion = tipoModificacion;
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

        public async Task<TipoIdentificacion> ObtenerXId(long Id)
        {
            return await appDbContext.TipoIdentificacion.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<TipoIdentificacion> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.TipoIdentificacion.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }
    }
}
