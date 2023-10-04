using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface ICategoriaProductosRepository
    {
        Task<IEnumerable<CategoriaProductos>> CargarDatos();
        Task<CategoriaProductos> ObtenerXId(long Id);
        Task<CategoriaProductos> ObtenerXNombre(string Nombre);
        Task<CategoriaProductos> Modificar(CategoriaProductos categoriaProductos);
        Task<CategoriaProductos> Agregar(CategoriaProductos categoriaProductos);
        Task Eliminar(long Id);
    }
}
