using EasySales.Shared;
using EasySales.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EasySales.Client.Services
{
    public class VentasService : IVentasService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly IClientesServices clientesServices;

        public VentasService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
            IClientesServices clientesServices)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.clientesServices = clientesServices;
        }

        public async Task<Facturas> Agregar(Dictionary<string, object> dictionary)
        {
            try
            {
                var user = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
                Facturas facturaInfo = (Facturas)dictionary["FacturaInfo"];
                List<VentaProductos> ventaDetalle = (List<VentaProductos>)dictionary["ProductosSeleccionados"];

                Facturas factura = new()
                {
                    Id=0,
                    EmpresaID = 2,
                    Empresa = null,
                    SucursalId = 1,
                    Sucursal = null,
                    NumeroFactura = "",
                    Fecha = DateTime.Now.Date,
                    EstadoFacturaId = 1,
                    EstadoFactura = null,
                    ClienteId = facturaInfo.ClienteId,//cliente.Id,
                    Cliente = null,
                    Subtotal = facturaInfo.Subtotal,
                    Impuestos = facturaInfo.Impuestos,
                    Total = facturaInfo.Total,
                    PromocionId = 0,
                    DescuentoValor=0,
                    UsuarioCreacion= user.Name,
                    FechaCreacion = DateTime.Now,
                    TipoModificacion = null,
                    TipoModificacionId = 0,
                };

               var data = new Dictionary<string, object>
                            {
                                {"Factura",factura},
                                {"VentaDetalle", ventaDetalle}
                            };

                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"/api/ventas", content);

                var nuevaFactura = await response.Content.ReadFromJsonAsync<Facturas>();

                return nuevaFactura;
            }
            catch (Exception EX)
            {

                throw;
            }
        }

        public async Task<Facturas> ObtenerXId(long id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Facturas>($"/api/ventas/{id}");
                if (response == null)
                {
                    return null;
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<FacturaDetalle>> ObtenerFacturaDetalleXId(long id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<IEnumerable<FacturaDetalle>>($"/api/ventas/facturadetalle/{id}");
                if (response == null)
                {
                    return Enumerable.Empty<FacturaDetalle>();
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
