using EasySales.Server.Data;
using EasySales.Server.Models.Interfaces;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext appDbContext;

        public UsersRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<EasySalesServerUser> Agregar(EasySalesServerUser claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                var resultado = await appDbContext.EasySalesServerUser.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EasySalesServerUser>> CargarDatos()
        {
            try
            {
                return await appDbContext.EasySalesServerUser.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        public async Task Eliminar(string Id)
        {
            var result = await appDbContext.EasySalesServerUser
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.EasySalesServerUser.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<EasySalesServerUser> Modificar(EasySalesServerUser claseEntrante)
        {
            try
            {
                var result = await appDbContext.EasySalesServerUser.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.NombreCompleto = claseEntrante.NombreCompleto;
                    result.NumeroIdentificacion = claseEntrante.NumeroIdentificacion;
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

        public async Task<EasySalesServerUser> ObtenerXId(string Id)
        {
            try
            {
                return await appDbContext.EasySalesServerUser.FirstOrDefaultAsync(e => e.Id == Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<EasySalesServerUser> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.EasySalesServerUser.FirstOrDefaultAsync(e => e.NombreCompleto == Nombre);
        }

        public async Task<EasySalesServerUser> ObtenerXIdentificacion(string Identificacion)
        {
            return await appDbContext.EasySalesServerUser.FirstOrDefaultAsync(e => e.NumeroIdentificacion == Identificacion);
        }

    }
}
