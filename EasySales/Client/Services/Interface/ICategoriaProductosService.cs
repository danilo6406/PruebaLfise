using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface ICategoriaProductosService
    {
        Task<IEnumerable<CategoriaProductos>> Buscar(string? Filtro);
        Task<IEnumerable<CategoriaProductos>> CargarDatos();
        Task<CategoriaProductos> ObtenerXId(long id);
        Task<CategoriaProductos> ObtenerXNombre(string Nombre);
        Task<CategoriaProductos> Agregar(CategoriaProductos categoriaProductos);
        Task<CategoriaProductos> Modificar(CategoriaProductos categoriaProductos);
        Task Eliminar(long id);
    }
}
