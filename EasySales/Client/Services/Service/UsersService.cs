using EasySales.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public UsersService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<EasySalesServerUser> Agregar(EasySalesServerUser claseEntrante)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                claseEntrante.UsuarioCreacion = user.Name;
                var response = await httpClient.PostAsJsonAsync<EasySalesServerUser>($"/api/users/", claseEntrante);
                return await response.Content.ReadFromJsonAsync<EasySalesServerUser>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<IEnumerable<EasySalesServerUser>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EasySalesServerUser>> CargarDatos()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<EasySalesServerUser>>("/api/users");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task Eliminar(string id)
        {
            await httpClient.DeleteAsync($"/api/users/{id}");
        }

        public async Task<EasySalesServerUser> Modificar(EasySalesServerUser claseEntrante)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            claseEntrante.UsuarioModificacion = user.Name;
            var response = await httpClient
            .PutAsJsonAsync<EasySalesServerUser>($"/api/users/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<EasySalesServerUser>();
        }

        public async Task<EasySalesServerUser> ObtenerXId(string id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<EasySalesServerUser>($"/api/users/{id}");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<EasySalesServerUser> ObtenerXIdentificacion(string Identificacion)
        {
            return await httpClient.GetFromJsonAsync<EasySalesServerUser>($"/api/users/buscarxnumid/{Identificacion}");
        }

        public async Task<EasySalesServerUser> ObtenerXNombre(string Nombre)
        {
            return await httpClient.GetFromJsonAsync<EasySalesServerUser>($"/api/users/buscarxnombre/{Nombre}");
        }

    }
}
