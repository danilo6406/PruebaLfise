using EasySales.Shared;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class CategoriaProductosService : ICategoriaProductosService
    {
        private readonly HttpClient httpClient;

        public CategoriaProductosService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CategoriaProductos> Agregar(CategoriaProductos CategoriaProductos)
        {
            var response = await httpClient.PostAsJsonAsync<CategoriaProductos>($"/api/CategoriaProductos", CategoriaProductos);
            return await response.Content.ReadFromJsonAsync<CategoriaProductos>();
        }

        public Task<IEnumerable<CategoriaProductos>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoriaProductos>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CategoriaProductos>>("/api/CategoriaProductos");
        }

        public async Task Eliminar(long id)
        {
            //throw new NotImplementedException();
            await httpClient.DeleteAsync($"/api/CategoriaProductos/{id}");
        }

        public async Task<CategoriaProductos> Modificar(CategoriaProductos categoriaProductos)
        {
            var response = await httpClient
            .PutAsJsonAsync<CategoriaProductos>($"/api/CategoriaProductos/{categoriaProductos.Id}", categoriaProductos);
            return await response.Content.ReadFromJsonAsync<CategoriaProductos>();
        }

        public async Task<CategoriaProductos> ObtenerXId(long id)
        {
            return await httpClient.GetFromJsonAsync<CategoriaProductos>($"/api/categoriaproductos/{id}");
        }

        public Task<CategoriaProductos> ObtenerXNombre(string Nombre)
        {
            throw new NotImplementedException();
        }
    }
}
