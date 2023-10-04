using EasySales.Shared;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class SubCategoriaProductosService : ISubCategoriaProductosService
    {
        private readonly HttpClient httpClient;

        public SubCategoriaProductosService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SubCategoriaProductos> Agregar(SubCategoriaProductos subCategoriaProductos)
        {
            long CategoriaId = subCategoriaProductos.CategoriaProductos.Id;
            subCategoriaProductos.CategoriaProductos = null;
            subCategoriaProductos.CategoriaProductosId = CategoriaId;
            var response = await httpClient.PostAsJsonAsync<SubCategoriaProductos>($"/api/subcategoriaproductos/", subCategoriaProductos);
            return await response.Content.ReadFromJsonAsync<SubCategoriaProductos>();
        }

        public Task<IEnumerable<SubCategoriaProductos>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SubCategoriaProductos>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<SubCategoriaProductos>>("/api/subcategoriaproductos/");
        }

        public async Task Eliminar(long id)
        {
            //throw new NotImplementedException();
            await httpClient.DeleteAsync($"/api/SubCategoriaProductos/{id}");
        }

        public async Task<SubCategoriaProductos> Modificar(SubCategoriaProductos subCategoriaProductos)
        {
            var response = await httpClient
            .PutAsJsonAsync<SubCategoriaProductos>($"/api/CategoriaProductos/{subCategoriaProductos.Id}", subCategoriaProductos);
            return await response.Content.ReadFromJsonAsync<SubCategoriaProductos>();
        }

        public async Task<SubCategoriaProductos> ObtenerXId(long id)
        {
            return await httpClient.GetFromJsonAsync<SubCategoriaProductos>($"/api/subcategoriaproductos/{id}");
        }

        public Task<SubCategoriaProductos> ObtenerXNombre(string Nombre)
        {
            throw new NotImplementedException();
        }

       
    }
}
