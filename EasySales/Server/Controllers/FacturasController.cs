using EasySales.Server.Models;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturasRepository facturasRepository;
        private readonly UserManager<EasySalesServerUser> userManager;

        public FacturasController(IFacturasRepository facturasRepository,UserManager<EasySalesServerUser> userManager)
        {
            this.facturasRepository = facturasRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> CargarDatos()
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return Ok(await facturasRepository.CargarDatos());
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

        [HttpGet]
        [Route("detallefactura/{FacturaId:int}")]
        public async Task<ActionResult> CargarDetalleFactura(long FacturaId)
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return Ok(await facturasRepository.CargarDetalleFactura(FacturaId));
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

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Facturas>> ObtenerXId(long Id)
        {
            try
            {
                var Resultado = await facturasRepository.ObtenerXId(Id);
                if (Resultado == null)
                {
                    return NotFound();
                }
                return Ok(Resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo la lista de productos desde el servidor");
            }
        }

        //[HttpGet("{ClienteId:int}")]
        //public async Task<ActionResult<Facturas>> ObtenerXClienteId(int ClienteId)
        //{
        //    try
        //    {
        //        var Resultado = await facturasRepository.ObtenerXClienteId(ClienteId);
        //        if (Resultado == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(Resultado);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo la lista de productos desde el servidor");
        //    }
        //}

        [HttpPut("{Id:int}")]
        [Route("anular/{Id:int}")]
        public async Task<ActionResult<Facturas>> Anular(int Id, Facturas claseEntrante)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }

                if (Id != claseEntrante.Id)
                {
                    return BadRequest("Numero de la factura no es valido");
                }

                var prod = await facturasRepository.ObtenerXId(Convert.ToInt64(Id));

                if (prod == null)
                {
                    return NotFound($"No existe una factura con el numero ={Id}");
                }

                var prodModificado = await facturasRepository.Anular(claseEntrante);

                return prodModificado;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del producto en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpDelete("{Id:int}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(long Id)
        {
            try
            {
                var prod = await facturasRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe una factura con el Id ={Id}");
                }

                await facturasRepository.Eliminar(Id);

                return Ok("Factura eliminada con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando l factura de la base de datos. Error: " + ex.Message);
            }
        }

    }
}
