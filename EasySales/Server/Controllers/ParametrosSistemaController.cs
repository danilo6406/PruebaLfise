using EasySales.Server.Models;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParametrosSistemaController : ControllerBase
    {
        private readonly IParametrosSistemaRepository parametrosSistemaRepository;

        public ParametrosSistemaController(IParametrosSistemaRepository parametrosSistemaRepository)
        {
            this.parametrosSistemaRepository = parametrosSistemaRepository;
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ParametrosSistema>> ObtenerXId(long Id)
        {
            try
            {
                var Resultado = await parametrosSistemaRepository.ObtenerXId(Id);
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


        [HttpGet("{Nombre}")]
        public async Task<ActionResult<ParametrosSistema>> ObtenerXNombre(string Nombre)
        {
            try
            {
                var Resultado = await parametrosSistemaRepository.ObtenerXNombre(Nombre);
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
                return Ok(await parametrosSistemaRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ParametrosSistema>> Agregar(ParametrosSistema parametrosSistema)
        {
            try
            {
                if (parametrosSistema == null)
                    return BadRequest();

                var cat = await parametrosSistemaRepository.ObtenerXNombre(parametrosSistema.Nombre);

                if (cat != null)
                {
                    ModelState.AddModelError("Nombre", "Ya existe una subcategoria con ese nombre.");
                    return BadRequest(ModelState);
                }

                var NuevoObjeto = await parametrosSistemaRepository.Agregar(parametrosSistema);

                return CreatedAtAction(nameof(ObtenerXId),
                    new { Id = NuevoObjeto.Id }, NuevoObjeto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nueva subcategoria en la base de datos.");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<ParametrosSistema>> Modificar(long Id, ParametrosSistema parametrosSistema)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }

                if (Id != parametrosSistema.Id)
                {
                    return BadRequest("Id del registro no es valido");
                }

                var prod = await parametrosSistemaRepository.ObtenerXId(Convert.ToInt64(Id));

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                return await parametrosSistemaRepository.Modificar(parametrosSistema);

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
                var prod = await parametrosSistemaRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await parametrosSistemaRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }

    }
}
