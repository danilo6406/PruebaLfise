using EasySales.Server.Models.Interfaces;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;

        public UsersController(IUsersRepository usersRepository )
        {
            this.usersRepository = usersRepository;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EasySalesServerUser>> ObtenerXId(string Id)
        {
            try
            {
                var Resultado = await usersRepository.ObtenerXId(Id);
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
        public async Task<ActionResult<EasySalesServerUser>> ObtenerXIdentificacion(string Identificacion)
        {
            try
            {
                var Resultado = await usersRepository.ObtenerXIdentificacion(Identificacion);
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
                return Ok(await usersRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EasySalesServerUser>> Agregar(EasySalesServerUser claseEntrante)
        {
            try
            {
                if (claseEntrante == null)
                    return BadRequest();

                var cat = await usersRepository.ObtenerXIdentificacion(claseEntrante.NumeroIdentificacion);

                if (cat != null)
                {
                    ModelState.AddModelError("NumeroIdentificacion", "Ya existe un registro con ese numero de identificacion.");
                    return BadRequest(ModelState);
                }

                var NuevoObjeto = await usersRepository.Agregar(claseEntrante);

                return CreatedAtAction(nameof(ObtenerXId),
                    new { Id = NuevoObjeto.Id }, NuevoObjeto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nuevo registro en la base de datos.");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EasySalesServerUser>> Modificar(string Id, EasySalesServerUser claseEntrante)
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

                var prod = await usersRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                return await usersRepository.Modificar(claseEntrante);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del registro en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(string Id)
        {
            try
            {
                var prod = await usersRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await usersRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }
    }
}
