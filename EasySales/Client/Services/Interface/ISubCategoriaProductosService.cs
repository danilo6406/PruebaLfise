using EasySales.Shared;

namespace EasySales.Client.Services
{
    public interface ISubCategoriaProductosService
    {
        Task<IEnumerable<SubCategoriaProductos>> Buscar(string? Filtro);
        Task<IEnumerable<SubCategoriaProductos>> CargarDatos();
        Task<SubCategoriaProductos> ObtenerXId(long id);
        Task<SubCategoriaProductos> ObtenerXNombre(string Nombre);
        Task<SubCategoriaProductos> Agregar(SubCategoriaProductos categoriaProductos);
        Task<SubCategoriaProductos> Modificar(SubCategoriaProductos categoriaProductos);
        Task Eliminar(long id);
    }
}
