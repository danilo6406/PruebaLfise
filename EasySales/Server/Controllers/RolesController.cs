using EasySales.Server.Models;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository rolesRepository;
        private readonly UserManager<EasySalesServerUser> userManager;

        public RolesController(IRolesRepository rolesRepository, UserManager<EasySalesServerUser> userManager)
        {
            this.rolesRepository = rolesRepository;
            this.userManager = userManager;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EasySalesServerRoles>> ObtenerXId(string Id)
        {
            try
            {
                var Resultado = await rolesRepository.ObtenerXId(Id);
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
        public async Task<ActionResult<EasySalesServerRoles>> ObtenerXIdentificacion(string codigo)
        {
            try
            {
                var Resultado = await rolesRepository.ObtenerXCodigo(codigo);
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
                return Ok(await rolesRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EasySalesServerRoles>> Agregar(EasySalesServerRoles claseEntrante)
        {
            try
            {
                if (claseEntrante == null)
                    return BadRequest();

                var cat = await rolesRepository.ObtenerXNombre(claseEntrante.Name);

                if (cat != null)
                {
                    ModelState.AddModelError("NumeroIdentificacion", "Ya existe un registro con ese nombre.");
                    return BadRequest(ModelState);
                }

                var user = await userManager.GetUserAsync(User);
                claseEntrante.EmpresaId = user.EmpresaId;

                var NuevoObjeto = await rolesRepository.Agregar(claseEntrante);

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
        public async Task<ActionResult<EasySalesServerRoles>> Modificar(string Id, EasySalesServerRoles claseEntrante)
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

                var prod = await rolesRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                return await rolesRepository.Modificar(claseEntrante);

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
                var prod = await rolesRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await rolesRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("obtenerxusuario/{id}")]
        public async Task<ActionResult> ObtenerXUsuario(string id)
        {
            try
            {
                var roles = (await rolesRepository.ObtenerXUsuario(id));
                return  Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }


        [Route("eliminarrolusuario/{IdRol}&{IdUser}")]
        public async Task<ActionResult> EliminarRolUsuario(string IdRol, string IdUser)
        {
            try
            {
                if (IdRol == null)
                {
                    return NotFound();
                }

                if (IdUser == null)
                {
                    return NotFound();
                }

                //if (Id != claseEntrante.Id)
                //{
                //    return BadRequest("Id del registro no es valido");
                //}

                var rol = await rolesRepository.ObtenerXId(IdRol);

                if (rol == null)
                {
                    return NotFound($"No existe un registro con el Id ={IdRol}");
                }

                await rolesRepository.EliminarRolUsuario(IdRol, IdUser);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del registro en la base de datos. Error: " + ex.Message);
            }
        }

        [Route("agregarrolusuario/{IdRol}&{IdUser}")]
        public async Task<ActionResult> AgregarRolUsuario(string IdRol, string IdUser)
        {
            try
            {
                if (IdRol == null)
                {
                    return NotFound();
                }

                if (IdUser == null)
                {
                    return NotFound();
                }

                //if (Id != claseEntrante.Id)
                //{
                //    return BadRequest("Id del registro no es valido");
                //}

                var rol = await rolesRepository.ObtenerXId(IdRol);

                if (rol == null)
                {
                    return NotFound($"No existe un registro con el Id ={IdRol}");
                }

                var respuesta = await rolesRepository.AgregarRolUsuario(IdRol, IdUser);

                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del registro en la base de datos. Error: " + ex.Message);
            }
        }
    }
}
