using EasySales.Shared;
using EasySales.Shared.ViewModels;

namespace EasySales.Client.Services
{
    public interface IProductosService
    {
        Task<IEnumerable<Productos>> Buscar(string? Filtro, int? ObjCategoriaProducto);
        Task<IEnumerable<Productos>> CargarProductos();
        Task<IEnumerable<ProductosDisponibles>> CargarProductosVentas();
        Task<IEnumerable<Productos>> CargarProductosReservas();
        Task<Productos> ObtenerProductoXId(long IdProducto);
        Task<Productos> ObtenerProductoXNombre(string Nombre);
        Task<Productos> ObtenerProductoXCategoriaProducto(int CategoriaProducto);
        Task<Productos> AgregarProducto(Productos Producto);
        Task<Productos> ModificarProducto(Productos Producto);
        Task EliminarProducto(long ProductoId);
    }
}
