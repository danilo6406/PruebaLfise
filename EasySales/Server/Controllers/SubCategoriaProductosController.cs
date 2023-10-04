using EasySales.Server.Models;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubCategoriaProductosController : ControllerBase
    {
        private readonly ISubCategoriaProductosRepository subCategoriaProductosRepository;

        public SubCategoriaProductosController(ISubCategoriaProductosRepository subCategoriaProductosRepository)
        {
            this.subCategoriaProductosRepository = subCategoriaProductosRepository;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<SubCategoriaProductos>> ObtenerXId(long Id)
        {
            try
            {
                var Resultado = await subCategoriaProductosRepository.ObtenerXId(Id);
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
                return Ok(await subCategoriaProductosRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoriaProductos>> Agregar(SubCategoriaProductos subCategoriaProductos)
        {
            try
            {
                if (subCategoriaProductos == null)
                    return BadRequest();

                var cat = await subCategoriaProductosRepository.ObtenerXNombre(subCategoriaProductos.Nombre);

                if (cat != null)
                {
                    ModelState.AddModelError("Nombre", "Ya existe una subcategoria con ese nombre.");
                    return BadRequest(ModelState);
                }

                var NuevoObjeto = await subCategoriaProductosRepository.Agregar(subCategoriaProductos);

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
        public async Task<ActionResult<SubCategoriaProductos>> Modificar(long Id, SubCategoriaProductos subCategoriaProductos)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }

                if (Id != subCategoriaProductos.Id)
                {
                    return BadRequest("Id del registro no es valido");
                }

                var prod = await subCategoriaProductosRepository.ObtenerXId(Convert.ToInt64(Id));

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                return await subCategoriaProductosRepository.Modificar(subCategoriaProductos);

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
                var prod = await subCategoriaProductosRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await subCategoriaProductosRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }

    }
}
