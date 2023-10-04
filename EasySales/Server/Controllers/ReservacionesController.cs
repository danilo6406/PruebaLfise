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
    public class ReservacionesController : ControllerBase
    {
        private readonly IReservacionesRepository reservacionesRepository;
        private readonly UserManager<EasySalesServerUser> userManager;

        public ReservacionesController(IReservacionesRepository reservacionesRepository, UserManager<EasySalesServerUser> userManager)
        {
            this.reservacionesRepository = reservacionesRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<Reservaciones>> Agregar(Reservaciones claseEntrante)
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (claseEntrante == null)
                            return BadRequest();                        

                        var user = await userManager.GetUserAsync(User);
                        claseEntrante.EmpresaId = user.EmpresaId;

                        var nueva = await reservacionesRepository.Agregar(claseEntrante);

                        return CreatedAtAction(nameof(ObtenerXId),
                            new { Id = nueva.Id }, nueva);
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nuevo producto en la base de datos.");
            }
        }

        [HttpPost]
        [Route("verificardisponibilidad")]
        public async Task<ActionResult<Reservaciones>> VerificarDisponibilidad(Reservaciones claseEntrante)
        {
            try
            {
                if (claseEntrante == null)
                    return BadRequest();

                long reservaExistenteId = await reservacionesRepository.ComprobarDisponibilidad(claseEntrante);

                if (reservaExistenteId != 0)
                {
                    Reservaciones reservaExistente =reservacionesRepository.ObtenerXId(reservaExistenteId).Result;

                    return CreatedAtAction(nameof(ObtenerXId),
                        new { Id = reservaExistenteId }, reservaExistente);
                }
                else
                {
                    Reservaciones reservaVacia=new();
                    return Ok(reservaVacia);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nuevo producto en la base de datos.");
            }
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
                        return Ok(await reservacionesRepository.CargarDatos());
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

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Reservaciones>> Modificar(int Id, Reservaciones reservaciones)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }

                if (Id != reservaciones.Id)
                {
                    return BadRequest("Id de resevarcion no es valido");
                }

                var reserva = await reservacionesRepository.ObtenerXId(Convert.ToInt32(Id));

                if (reserva == null)
                {
                    return NotFound($"No existe una reservacion con el Id ={Id}");
                }

                var reservadModificado = await reservacionesRepository.Modificar(reservaciones);

                return reservadModificado;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del producto en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("anular/{Id}")]
        public async Task<IActionResult> Anular(int Id)
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        await reservacionesRepository.Anular(Id);
                        return Ok("Anulada");
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
        public async Task<ActionResult<Reservaciones>> ObtenerXId(long Id)
        {
            try
            {
                var Resultado = await reservacionesRepository.ObtenerXId(Id);
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






    }
}
