using EasySales.Server.Models;
using EasySales.Server.Models.Interfaces;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepository clientesRepository;

        public ClientesController(IClientesRepository clientesRepository)
        {
            this.clientesRepository = clientesRepository;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Clientes>> ObtenerXId(long Id)
        {
            try
            {
                var Resultado = await clientesRepository.ObtenerXId(Id);
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

        [HttpGet]
        [Route("BuscarXNumId/{Identificacion}")] 
        public async Task<ActionResult<Clientes>> ObtenerXIdentificacion(string Identificacion)
        {
            try
            {
                var Resultado = await clientesRepository.ObtenerXIdentificacion(Identificacion);
                if (Resultado == null)
                {
                    return NotFound();
                }
                return Ok(Resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error cargando registro por numero de identificacion desde el servidor");
            }
        }

        [HttpGet]
        public async Task<ActionResult> CargarDatos()
        {
            try
            {
                return Ok(await clientesRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Clientes>> Agregar(Clientes claseEntrante)
        {
            try
            {
                if (claseEntrante == null)
                    return BadRequest();

                var cat = await clientesRepository.ObtenerXIdentificacion(claseEntrante.NumeroIdentificacion);

                if (cat != null)
                {
                    ModelState.AddModelError("NumeroIdentificacion", "Ya existe un registro con ese numero de identificacion.");
                    return BadRequest(ModelState);
                }

                var NuevoObjeto = await clientesRepository.Agregar(claseEntrante);

                return CreatedAtAction(nameof(ObtenerXId),
                    new { Id = NuevoObjeto.Id }, NuevoObjeto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nuevo registro en la base de datos.");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Clientes>> Modificar(long Id, Clientes claseEntrante)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }

                if (Id != claseEntrante.Id)
                {
                    return BadRequest("Id del registro no es valido");
                }

                var prod = await clientesRepository.ObtenerXId(Convert.ToInt64(Id));

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                return await clientesRepository.Modificar(claseEntrante);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del registro en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpDelete("{Id:int}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(long Id)
        {
            try
            {
                var prod = await clientesRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await clientesRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }

    }
}
