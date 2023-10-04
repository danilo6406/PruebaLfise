using EasySales.Server.Models;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BodegasController : ControllerBase
    {
        private readonly IBodegasRepository bodegasRepository;

        public BodegasController(IBodegasRepository bodegasRepository)
        {
            this.bodegasRepository = bodegasRepository;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Bodegas>> ObtenerXId(int Id)
        {
            try
            {
                var Resultado = await bodegasRepository.ObtenerXId(Id);
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
        public async Task<ActionResult> CargarDatos()
        {
            try
            {
                return Ok(await bodegasRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Bodegas>> Agregar(Bodegas claseEntrante)
        {
            try
            {
                if (claseEntrante == null)
                    return BadRequest();

                var cat = await bodegasRepository.ObtenerXNombre(claseEntrante.Nombre);

                if (cat != null)
                {
                    ModelState.AddModelError("Nombre", "Ya existe un registro con ese nombre.");
                    return BadRequest(ModelState);
                }

                var NuevoObjeto = await bodegasRepository.Agregar(claseEntrante);

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
        public async Task<ActionResult<Bodegas>> Modificar(int Id, Bodegas claseEntrante)
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

                var prod = await bodegasRepository.ObtenerXId(Convert.ToInt32(Id));

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                return await bodegasRepository.Modificar(claseEntrante);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del registro en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpDelete("{Id:int}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int Id)
        {
            try
            {
                var prod = await bodegasRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await bodegasRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }

    }
}
