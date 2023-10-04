using EasySales.Shared;
using System.Net.Http.Json;

namespace EasySales.Client.Services
{
    public class FacturasService : IFacturasService
    {
        private readonly HttpClient httpClient;

        public FacturasService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Facturas>> CargarDatos()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Facturas>>("/api/facturas");
        }

        public async Task<IEnumerable<FacturaDetalle>> CargarDetalleFactura(long FacturaId)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<FacturaDetalle>>("/api/facturas/detallefactura/" + FacturaId.ToString());
        }

        public async Task Eliminar(int id)
        {
            await httpClient.DeleteAsync($"/api/facturas/{id}");
        }

        public async Task<Facturas> Modificar(Facturas claseEntrante)
        {
            var response = await httpClient
            .PutAsJsonAsync<Facturas>($"/api/facturas/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<Facturas>();
        }

        public async Task<Facturas> Anular(Facturas claseEntrante)
        {
            var response = await httpClient
            .PutAsJsonAsync<Facturas>($"/api/facturas/anular/{claseEntrante.Id}", claseEntrante);
            return await response.Content.ReadFromJsonAsync<Facturas>();
        }

        public async Task<Facturas> ObtenerXId(long Id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Facturas>($"/api/facturas/{Id}");
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
