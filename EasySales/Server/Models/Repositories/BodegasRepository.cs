using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class BodegasRepository : IBodegasRepository
    {
        private readonly AppDbContext appDbContext;

        public BodegasRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Bodegas> Agregar(Bodegas claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                claseEntrante.FechaCreacion = DateTime.Now;
                var resultado = await appDbContext.Bodegas.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Bodegas>> CargarDatos()
        {
            try
            {
                return await appDbContext.Bodegas.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Eliminar(int Id)
        {
            var result = await appDbContext.Bodegas
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.Bodegas.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Bodegas> Modificar(Bodegas claseEntrante)
        {
            try
            {
                var result = await appDbContext.Bodegas.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
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

        public async Task<Bodegas> ObtenerXId(int Id)
        {
            try
            {
                return await appDbContext.Bodegas.FirstOrDefaultAsync(e => e.Id == Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Bodegas> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.Bodegas.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }

    }
}
