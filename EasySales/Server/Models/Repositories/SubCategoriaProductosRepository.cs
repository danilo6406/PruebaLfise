using EasySales.Server.Data;
using EasySales.Shared;
using EasySales.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Models
{
    public class SubCategoriaProductosRepository : ISubCategoriaProductosRepository
    {
        private readonly AppDbContext appDbContext;

        public SubCategoriaProductosRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<SubCategoriaProductos>> CargarDatos()
        {
            try
            {
                return await appDbContext.SubCategoriaProductos.Include(e => e.CategoriaProductos).ToListAsync();
                //return await appDbContext.SubCategoriaProductos.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SubCategoriaProductos> ObtenerXId(long Id)
        {
            return await appDbContext.SubCategoriaProductos.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<SubCategoriaProductos> ObtenerXNombre(string Nombre)
        {
            return await appDbContext.SubCategoriaProductos.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }

        public async Task<SubCategoriaProductos> Modificar(SubCategoriaProductos subCategoriaProductos)
        {
            var result = await appDbContext.SubCategoriaProductos.FirstOrDefaultAsync(e => e.Id == subCategoriaProductos.Id);
            var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");

            if (result != null)
            {
                result.Nombre = subCategoriaProductos.Nombre;
                result.Descripcion = subCategoriaProductos.Descripcion;
                result.Activo = subCategoriaProductos.Activo;
                result.FechaCreacion = subCategoriaProductos.FechaCreacion;
                result.UsuarioCreacion = subCategoriaProductos.UsuarioCreacion;
                result.FechaModificacion = DateTime.Now;
                result.UsuarioModificacion = subCategoriaProductos.UsuarioModificacion;
                result.Id = subCategoriaProductos.Id;
                result.TipoModificacion = tipoModificacion;
                result.TipoModificacionId = tipoModificacion.Id;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public async Task<SubCategoriaProductos> Agregar(SubCategoriaProductos subCategoriaProductos)
        {
            //Cuando se guarda un producto con una propiedad con clase pero no se quiere actualizar la tabla de la clase.
            //if (Producto.Department != null)
            //{
            //    appDbContext.Entry(Producto.Department).State = EntityState.Unchanged;
            //}
            if (subCategoriaProductos.CategoriaProductosId==0)
            {
                throw new Exception("Categoria de producto no puede ir vacio.");
            }
            var resultado = await appDbContext.SubCategoriaProductos.AddAsync(subCategoriaProductos);
            await appDbContext.SaveChangesAsync();
            return resultado.Entity;
        }

        public async Task Eliminar(long Id)
        {
            var result = await appDbContext.SubCategoriaProductos
               .FirstOrDefaultAsync(e => e.Id == Id);

            if (result != null)
            {
                appDbContext.SubCategoriaProductos.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

    }
}
