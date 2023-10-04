using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class RolesRepository : IRolesRepository
    {

        private readonly AppDbContext appDbContext;
        private readonly UserManager<EasySalesServerUser> userManager;
        private readonly RoleManager<EasySalesServerRoles> roleManager;

        public RolesRepository(AppDbContext appDbContext, UserManager<EasySalesServerUser> userManager,RoleManager<EasySalesServerRoles> roleManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<EasySalesServerRoles> Agregar(EasySalesServerRoles claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacionId = tipoModificacion.Id;
                claseEntrante.FechaCreacion = DateTime.Now;
                claseEntrante.NormalizedName = claseEntrante.Name.ToUpper();
                var resultado = await appDbContext.EasySalesServerRoles.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EasySalesServerRoles>> CargarDatos()
        {
            try
            {
                return await appDbContext.EasySalesServerRoles.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Eliminar(string Id)
        {
            var result = await appDbContext.EasySalesServerRoles
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.EasySalesServerRoles.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<EasySalesServerRoles> Modificar(EasySalesServerRoles claseEntrante)
        {
            try
            {
                var result = await appDbContext.EasySalesServerRoles.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.Name = claseEntrante.Name;
                    result.NormalizedName = claseEntrante.NormalizedName;
                    result.Codigo = claseEntrante.Codigo;
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

        public async Task<EasySalesServerRoles> ObtenerXId(string Id)
        {
            try
            {
                return await appDbContext.EasySalesServerRoles.FirstOrDefaultAsync(e => e.Id == Id);
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        public async Task<EasySalesServerRoles> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.EasySalesServerRoles.FirstOrDefaultAsync(e => e.Name == Nombre);
        }

        public async Task<EasySalesServerRoles> ObtenerXCodigo(string codigo)
        {
            return await appDbContext.EasySalesServerRoles.FirstOrDefaultAsync(e => e.Codigo == codigo);
        }

        public async Task<IEnumerable<EasySalesServerRoles>> ObtenerXUsuario(string Id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);
                var roles = await userManager.GetRolesAsync(user);
                var rolesIEnumerable = new List<EasySalesServerRoles>();

                foreach (var roleName in roles)
                {
                    var role = await roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        rolesIEnumerable.Add(role);
                    }
                }

                return rolesIEnumerable;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task EliminarRolUsuario(string IdRol, string IdUser)
        {
            try
            {
                var user = await userManager.FindByIdAsync(IdUser);
                var rol = await roleManager.FindByIdAsync(IdRol);
                await userManager.RemoveFromRoleAsync(user,rol.Name);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> AgregarRolUsuario(string IdRol, string IdUser)
        {
            try
            {
                var user = await userManager.FindByIdAsync(IdUser);
                var rol = await roleManager.FindByIdAsync(IdRol);
                var rolCheck = await userManager.IsInRoleAsync(user,rol.Name);
                if(rolCheck == false)
                {
                    await userManager.AddToRoleAsync(user, rol.Name);
                    return "Rol agregado con exito";
                }
                else
                {
                   return "El usuario ya tiene ese rol asignado";
                }
                
            }
            catch (Exception ex)
            {
                return "Hubo un problema asignado el rol.  Error: " + ex.Message;
                throw;
            }
        }
    }
}
