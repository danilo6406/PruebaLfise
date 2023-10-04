using EasySales.Server.Models;
using EasySales.Shared;
using EasySales.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace EasySales.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentasRepository ventasRepository;
        private readonly UserManager<EasySalesServerUser> userManager;

        public VentasController(IVentasRepository ventasRepository, UserManager<EasySalesServerUser> userManager)
        {
            this.ventasRepository = ventasRepository;
            this.userManager = userManager;
        }

        [HttpGet("{id}")]        
        public async Task<ActionResult<Facturas>> ObtenerXId(long id)
        {
            try
            {
                var Resultado = await ventasRepository.ObtenerXId(id);
                if (Resultado == null)
                {
                    return NotFound();
                }
                return Ok(Resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error cargando registro por id desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Facturas>> Agregar([FromBody] JsonElement dataElement)
        {
            try
            {
                Dictionary<string, JsonElement> data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(dataElement.GetRawText());
                if (data.ContainsKey("Factura") && data.ContainsKey("VentaDetalle"))
                {

                    // Deserialize the 'Factura' object
                    Facturas factura = JsonSerializer.Deserialize<Facturas>(data["Factura"].GetRawText());

                    // Deserialize the 'ventaDetalle' object
                    List<VentaProductos> ventaDetalle = JsonSerializer.Deserialize<List<VentaProductos>>(data["VentaDetalle"].GetRawText());


                    if (factura == null)
                        return BadRequest();

                    if (ventaDetalle == null)
                        return BadRequest();

                    var user = await userManager.GetUserAsync(User);

                    factura.UsuarioCreacion = User.Identity.Name;
                    factura.EmpresaID = user.EmpresaId;
                    factura.SucursalId = user.SucursalId;
                    factura.CajaId = 1;

                    //Validacion de factura duplicada PENDIENTE
                    //var cat = await ventasRepository.ObtenerXId(tipoIdentificacion.Nombre);

                    //if (cat != null)
                    //{
                    //    ModelState.AddModelError("Nombre", "Ya existe una subcategoria con ese nombre.");
                    //    return BadRequest(ModelState);
                    //}

                    var NuevoObjeto = await ventasRepository.Agregar(factura);

                    long idNuevaFactura = NuevoObjeto.Id;

                    foreach (var item in ventaDetalle)
                    {
                        FacturaDetalle facturaDetalle = new()
                        {
                            EmpresaId = factura.EmpresaID,
                            FacturaId = idNuevaFactura,
                            ProductoId = item.Id,
                            Cantidad = item.Cantidad,
                            Subtotal = item.Subtotal,
                            Impuestos = item.Impuesto,
                            EstadoFacturaId = factura.EstadoFacturaId,
                            UsuarioCreacion = User.Identity.Name,
                            FechaCreacion = DateTime.Now,
                            TipoModificacionId = 1
                        };
                         await ventasRepository.AgregarFacturaDetalle(facturaDetalle);
                    }

                    return CreatedAtAction(nameof(ObtenerXId),
                    new { Id = NuevoObjeto.Id }, NuevoObjeto);


                }
                else
                {
                    return BadRequest("Formato de datos invalido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nueva registro en la base de datos.");
            }
        }

        [HttpGet]
        [Route("facturadetalle/{id}")]
        public async Task<ActionResult> ObtenerFacturaDetalleXId(long id)
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return Ok(await ventasRepository.ObtenerFacturaDetalleXId(id));
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo la lista de productos desde el servidor");
            }
        }
    }
}
