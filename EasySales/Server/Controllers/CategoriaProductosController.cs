using EasySales.Server.Models;
using EasySales.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaProductosController : ControllerBase
    {
        private readonly ICategoriaProductosRepository categoriaProductosRepository;

        public CategoriaProductosController(ICategoriaProductosRepository categoriaProductosRepository)
        {
            this.categoriaProductosRepository = categoriaProductosRepository;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CategoriaProductos>> ObtenerXId(long Id)
        {
            try
            {
                var Resultado = await categoriaProductosRepository.ObtenerXId(Id);
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
                return Ok(await categoriaProductosRepository.CargarDatos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error obteniendo datos desde el servidor");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<CategoriaProductos>> Modificar(long Id, CategoriaProductos categoriaProductos)
        {
            try
            {
                if (Id == null)
                {
                    return NotFound();
                }

                if (Id != categoriaProductos.Id)
                {
                    return BadRequest("Id del producto no es valido");
                }

                var prod = await categoriaProductosRepository.ObtenerXId(Convert.ToInt64(Id));

                if (prod == null)
                {
                    return NotFound($"No existe una categoria con el Id ={Id}");
                }

                return await categoriaProductosRepository.Modificar(categoriaProductos);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del producto en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaProductos>> Agregar(CategoriaProductos categoriaProductos)
        {
            try
            {
                if (categoriaProductos == null)
                    return BadRequest();

                var cat = await categoriaProductosRepository.ObtenerXNombre(categoriaProductos.Nombre);

                if (cat != null)
                {
                    ModelState.AddModelError("Nombre", "Ya existe una categoria con ese nombre.");
                    return BadRequest(ModelState);
                }

                var NuevoObjeto = await categoriaProductosRepository.Agregar(categoriaProductos);

                return CreatedAtAction(nameof(ObtenerXId),
                    new { Id = NuevoObjeto.Id }, NuevoObjeto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creando nueva categoria en la base de datos.");
            }
        }

        [HttpDelete("{Id:int}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(long Id)
        {
            try
            {
                var prod = await categoriaProductosRepository.ObtenerXId(Id);

                if (prod == null)
                {
                    return NotFound($"No existe un registro con el Id ={Id}");
                }

                await categoriaProductosRepository.Eliminar(Id);

                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el registro de la base de datos. Error: " + ex.Message);
            }
        }

    }
}
