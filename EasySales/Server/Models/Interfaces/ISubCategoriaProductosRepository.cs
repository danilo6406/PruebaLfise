using EasySales.Shared;

namespace EasySales.Server.Models
{
    public interface ISubCategoriaProductosRepository
    {
        Task<IEnumerable<SubCategoriaProductos>> CargarDatos();
        Task<SubCategoriaProductos> ObtenerXId(long Id);
        Task<SubCategoriaProductos> ObtenerXNombre(string Nombre);
        Task<SubCategoriaProductos> Modificar(SubCategoriaProductos categoriaProductos);
        Task<SubCategoriaProductos> Agregar(SubCategoriaProductos categoriaProductos);
        Task Eliminar(long Id);
    }
}
