using EasySales.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class ClientesService : IClientesServices
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public ClientesService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider )
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Clientes> Agregar(Clientes claseEntrante)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                claseEntrante.TipoIdentificacionId = claseEntrante.TipoIdentificacion.Id;
                claseEntrante.TipoIdentificacion = null;
                claseEntrante.TipoModificacion = null;
                claseEntrante.UsuarioCreacion = user.Name;
                var response = await httpClient.PostAsJsonAsync<Clientes>($"/api/clientes/", claseEntrante);
                return await response.Content.ReadFromJsonAsync<Clientes>();
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        public Task<IEnumerable<Clientes>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Clientes>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Clientes>>("/api/clientes");
        }

        public async Task Eliminar(long id)
        {
            await httpClient.DeleteAsync($"/api/clientes/{id}");
        }

        public async Task<Clientes> Modificar(Clientes claseEntrante)
        {
            var response = await httpClient
            .PutAsJsonAsync<Clientes>($"/api/clientes/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<Clientes>();
        }

        public async Task<Clientes> ObtenerXId(long id)
        {
            return await httpClient.GetFromJsonAsync<Clientes>($"/api/clientes/{id}");
        }

        public async Task<Clientes> ObtenerXIdentificacion(string Identificacion)
        {
            return await httpClient.GetFromJsonAsync<Clientes>($"/api/clientes/buscarxnumid/{Identificacion}");
        }

        public async Task<Clientes> ObtenerXNombre(string Nombre)
        {
            return await httpClient.GetFromJsonAsync<Clientes>($"/api/clientes/buscarxnombre/{Nombre}");
        }
    }
}
