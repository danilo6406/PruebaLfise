using EasySales.Shared;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace EasySales.Client.Services
{
    public class ParametrosSistemaService : IParametrosSistemaService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public ParametrosSistemaService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ParametrosSistema> Agregar(ParametrosSistema claseEntrante)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            claseEntrante.UsuarioCreacion = user.Name;
            var response = await httpClient.PostAsJsonAsync<ParametrosSistema>($"/api/parametrossistema/", claseEntrante);
            return await response.Content.ReadFromJsonAsync<ParametrosSistema>();
        }

        public Task<IEnumerable<ParametrosSistema>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParametrosSistema>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ParametrosSistema>>("/api/parametrossistema");
        }

        public async Task Eliminar(long id)
        {
            await httpClient.DeleteAsync($"/api/parametrossistema/{id}");
        }

        public async Task<ParametrosSistema> Modificar(ParametrosSistema claseEntrante)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            claseEntrante.UsuarioCreacion = user.Name;
            var response = await httpClient
            .PutAsJsonAsync<ParametrosSistema>($"/api/parametrossistema/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<ParametrosSistema>();
        }

        public async Task<ParametrosSistema> ObtenerXId(long id)
        {
            return await httpClient.GetFromJsonAsync<ParametrosSistema>($"/api/parametrossistema/{id}");
        }

        public async Task<ParametrosSistema> ObtenerXNombre(string Nombre)
        {
            return await httpClient.GetFromJsonAsync<ParametrosSistema>($"/api/parametrossistema/{Nombre}");
        }

    }

}
