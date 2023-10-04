using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class CategoriaProductosRepository : ICategoriaProductosRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoriaProductosRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<CategoriaProductos>> CargarDatos()
        {
            return await appDbContext.CategoriaProductos.ToListAsync();
        }

        public async Task<CategoriaProductos> ObtenerXId(long IdCategoria)
        {
            return await appDbContext.CategoriaProductos.FirstOrDefaultAsync(e => e.Id == IdCategoria);
        }

        public async Task<CategoriaProductos> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.CategoriaProductos.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }

        public async Task<CategoriaProductos> Modificar(CategoriaProductos categoriaProductos)
        {
            try
            {
                var result = await appDbContext.CategoriaProductos.FirstOrDefaultAsync(e => e.Id == categoriaProductos.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

                if (result != null)
                {
                    result.Nombre = categoriaProductos.Nombre;
                    result.Descripcion = categoriaProductos.Descripcion;
                    result.Activo = categoriaProductos.Activo;
                    result.FechaCreacion = categoriaProductos.FechaCreacion;
                    result.UsuarioCreacion = categoriaProductos.UsuarioCreacion;
                    result.FechaModificacion = DateTime.Now;
                    result.UsuarioModificacion = categoriaProductos.UsuarioModificacion;
                    result.Id = categoriaProductos.Id;
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

        public async Task<CategoriaProductos> Agregar(CategoriaProductos categoriaProductos)
        {
            //Cuando se guarda un producto con una propiedad con clase pero no se quiere actualizar la tabla de la clase.
            //if (Producto.Department != null)
            //{
            //    appDbContext.Entry(Producto.Department).State = EntityState.Unchanged;
            //}
            var resultado = await appDbContext.CategoriaProductos.AddAsync(categoriaProductos);
            await appDbContext.SaveChangesAsync();
            return resultado.Entity;
        }

        public async Task Eliminar(long Id)
        {
            var result = await appDbContext.CategoriaProductos
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.CategoriaProductos.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
