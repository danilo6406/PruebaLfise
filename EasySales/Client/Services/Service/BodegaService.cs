using EasySales.Shared;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class BodegaService : IBodegaService
    {
        private readonly HttpClient httpClient;

        public BodegaService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Bodegas> Agregar(Bodegas claseEntrante)
        {
            var response = await httpClient.PostAsJsonAsync<Bodegas>($"/api/bodegas/", claseEntrante);
            return await response.Content.ReadFromJsonAsync<Bodegas>();
        }

        public Task<IEnumerable<Bodegas>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bodegas>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Bodegas>>("/api/bodegas");
        }

        public async Task Eliminar(int id)
        {
            await httpClient.DeleteAsync($"/api/bodegas/{id}");
        }

        public async Task<Bodegas> Modificar(Bodegas claseEntrante)
        {
            var response = await httpClient
            .PutAsJsonAsync<Bodegas>($"/api/bodegas/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<Bodegas>();
        }

        public Task<TipoIdentificacion> ObtenerXId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TipoIdentificacion> ObtenerXNombre(string Nombre)
        {
            throw new NotImplementedException();
        }
    }
}
