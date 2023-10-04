using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class ParametroSistemaRepository : IParametrosSistemaRepository
    {
        private readonly AppDbContext appDbContext;

        public ParametroSistemaRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ParametrosSistema> Agregar(ParametrosSistema claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacion = tipoModificacion;
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                var resultado = await appDbContext.ParametrosSistema.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ParametrosSistema>> CargarDatos()
        {
            return await appDbContext.ParametrosSistema.ToListAsync();
        }

        public async Task Eliminar(long Id)
        {
            var result = await appDbContext.ParametrosSistema
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.ParametrosSistema.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<ParametrosSistema> Modificar(ParametrosSistema claseEntrante)
        {
            try
            {
                var result = await appDbContext.ParametrosSistema.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.Nombre = claseEntrante.Nombre;
                    result.Descripcion = claseEntrante.Descripcion;
                    result.Activo = claseEntrante.Activo;
                    result.ValorNumerico = claseEntrante.ValorNumerico;
                    result.ValorString = claseEntrante.ValorString;
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

        public async Task<ParametrosSistema> ObtenerXId(long Id)
        {
            return await appDbContext.ParametrosSistema.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<ParametrosSistema> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.ParametrosSistema.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }

        public async Task<string> ObtenerValorStringXNombre(string Nombre)
        {
            var parametro = await appDbContext.ParametrosSistema.FirstOrDefaultAsync(e => e.Nombre == Nombre);
            return parametro?.ValorString ?? "";
        }
        public async Task<decimal> ObtenerValorNumericoXNombre(string Nombre)
        {
            var parametro = await appDbContext.ParametrosSistema.FirstOrDefaultAsync(e => e.Nombre == Nombre);
            return parametro?.ValorNumerico ?? 0;
        }

    }
}
