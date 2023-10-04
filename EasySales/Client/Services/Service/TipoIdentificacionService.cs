using EasySales.Shared;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class TipoIdentificacionService : ITipoIdentificacionService
    {
        private readonly HttpClient httpClient;

        public TipoIdentificacionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TipoIdentificacion> Agregar(TipoIdentificacion claseEntrante)
        {
            var response = await httpClient.PostAsJsonAsync<TipoIdentificacion>($"/api/tipoidentificacion/", claseEntrante);
            return await response.Content.ReadFromJsonAsync<TipoIdentificacion>();
        }

        public Task<IEnumerable<TipoIdentificacion>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TipoIdentificacion>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<TipoIdentificacion>>("/api/tipoidentificacion");
        }

        public async Task Eliminar(long id)
        {
            await httpClient.DeleteAsync($"/api/tipoidentificacion/{id}");
        }

        public async Task<TipoIdentificacion> Modificar(TipoIdentificacion claseEntrante)
        {
            var response = await httpClient
            .PutAsJsonAsync<TipoIdentificacion>($"/api/tipoidentificacion/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<TipoIdentificacion>();
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
