using EasySales.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;


namespace EasySales.Client.Services
{
    public class RolesService : IRolesService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public RolesService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<EasySalesServerRoles> Agregar(EasySalesServerRoles claseEntrante)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                claseEntrante.UsuarioCreacion = user.Name;
                var response = await httpClient.PostAsJsonAsync<EasySalesServerRoles>($"/api/roles/", claseEntrante);
                var newObjeto = await response.Content.ReadFromJsonAsync<EasySalesServerRoles>();
                return newObjeto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<IEnumerable<EasySalesServerRoles>> Buscar(string? Filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EasySalesServerRoles>> CargarDatos()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<EasySalesServerRoles>>("/api/roles");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task Eliminar(string id)
        {
            try
            {
                await httpClient.DeleteAsync($"/api/roles/{id}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EasySalesServerRoles> Modificar(EasySalesServerRoles claseEntrante)
        {
            var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            claseEntrante.UsuarioModificacion = user.Name;
            var response = await httpClient
            .PutAsJsonAsync<EasySalesServerRoles>($"/api/roles/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<EasySalesServerRoles>();
        }

        public async Task<EasySalesServerRoles> ObtenerXId(long id)
        {
            return await httpClient.GetFromJsonAsync<EasySalesServerRoles>($"/api/roles/{id}");
        }

        public async Task<EasySalesServerRoles> ObtenerXCodigo(string codigo)
        {
            return await httpClient.GetFromJsonAsync<EasySalesServerRoles>($"/api/users/buscarxnumid/{codigo}");
        }

        public async Task<EasySalesServerRoles> ObtenerXNombre(string Nombre)
        {
            return await httpClient.GetFromJsonAsync<EasySalesServerRoles>($"/api/roles/buscarxnombre/{Nombre}");
        }

        public async Task<IEnumerable<EasySalesServerRoles>> ObtenerXUsuario(string id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<EasySalesServerRoles>>($"/api/roles/obtenerxusuario/{id}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task EliminarRolUsuario(string IdRol, string IdUser)
        {
            try
            {
                await httpClient.GetAsync($"/api/roles/eliminarrolusuario/{IdRol}&{IdUser}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> AgregarRolUsuario(string IdRol, string IdUser)
        {
            try
            {
               var response = await httpClient.GetAsync($"/api/roles/agregarrolusuario/{IdRol}&{IdUser}");
                return response.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw;
            }
        }
    }
}
