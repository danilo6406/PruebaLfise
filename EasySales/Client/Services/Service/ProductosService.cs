using EasySales.Shared;
using EasySales.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class ProductosService : IProductosService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public ProductosService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Productos> AgregarProducto(Productos Producto)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                Producto.UsuarioCreacion = user.Name;
                Producto.FechaCreacion = DateTime.Now;
                var response = await httpClient.PostAsJsonAsync<Productos>("/api/productos", Producto);
                return await response.Content.ReadFromJsonAsync<Productos>();
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public Task<IEnumerable<Productos>> Buscar(string? Filtro, int? ObjCategoriaProducto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Productos>> CargarProductos()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<IEnumerable<Productos>>("/api/productos");
                if (response == null)
                {
                    return Enumerable.Empty<Productos>();
                }
                else
                {
                    return response;
                }
                 
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Productos>();
                throw;
            }            
        }

        public async Task<IEnumerable<ProductosDisponibles>> CargarProductosVentas()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<IEnumerable<ProductosDisponibles>>("/api/productos/ventas/");
                if (response == null)
                {
                    return Enumerable.Empty<ProductosDisponibles>();
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Productos>> CargarProductosReservas()
        { 
            try
            {
                var response = await httpClient.GetFromJsonAsync<IEnumerable<Productos>>("/api/productos/reservas/");
                if (response == null)
                {
                    return Enumerable.Empty<Productos>();
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task EliminarProducto(long ProductoId)
        {
            try
            {
                //throw new NotImplementedException();
                await httpClient.DeleteAsync($"/api/productos/{ProductoId}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Productos> ModificarProducto(Productos Producto)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                Producto.UsuarioModificacion = user.Name;
                var response = await httpClient.PutAsJsonAsync<Productos>($"/api/productos/{Producto.Id}", Producto);
                return await response.Content.ReadFromJsonAsync<Productos>();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public Task<Productos> ObtenerProductoXCategoriaProducto(int CategoriaProducto)
        {
            throw new NotImplementedException();
        }

        public async Task<Productos> ObtenerProductoXId(long IdProducto)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Productos>($"/api/productos/{IdProducto}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Productos> ObtenerProductoXNombre(string Nombre)
        {
            throw new NotImplementedException();
        }
    }
}
