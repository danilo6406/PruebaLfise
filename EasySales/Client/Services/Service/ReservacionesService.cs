using EasySales.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EasySales.Client.Services
{
    public class ReservacionesService : IReservacionesService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public ReservacionesService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Reservaciones> Agregar(Reservaciones claseEntrante)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                claseEntrante.UsuarioCreacion = user.Name;
                claseEntrante.FechaCreacion = DateTime.Now;
                var response = await httpClient.PostAsJsonAsync<Reservaciones>("/api/reservaciones", claseEntrante);
                return await response.Content.ReadFromJsonAsync<Reservaciones>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Reservaciones> VerificarDisponibilidad(Reservaciones claseEntrante)
        {
            try
            {
                claseEntrante.UsuarioCreacion = "Sistema";
                claseEntrante.FechaCreacion = DateTime.Now;
                var response = await httpClient.PostAsJsonAsync<Reservaciones>("/api/reservaciones/verificardisponibilidad", claseEntrante);
                return await response.Content.ReadFromJsonAsync<Reservaciones>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Reservaciones>> CargarDatos()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<IEnumerable<Reservaciones>>("/api/reservaciones");
                if (response == null)
                {
                    return Enumerable.Empty<Reservaciones>();
                }
                else
                {
                    return response;
                }

            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Reservaciones>();
                throw;
            }
        }

        public async Task Anular(long Id)
        {
            try
            {
                //throw new NotImplementedException();
                await httpClient.GetAsync($"/api/reservaciones/anular/{Id}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Reservaciones> Modificar(Reservaciones claseEntrante)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                claseEntrante.UsuarioModificacion = user.Name;
                var response = await httpClient.PutAsJsonAsync<Reservaciones>($"/api/reservaciones/{claseEntrante.Id}", claseEntrante);
                return await response.Content.ReadFromJsonAsync<Reservaciones>();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public async Task<Reservaciones> ObtenerXId(long Id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Reservaciones>($"/api/Reservaciones/{Id}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        

    }
}
