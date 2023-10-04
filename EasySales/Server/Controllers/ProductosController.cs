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
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepository productosRepository;
        private readonly UserManager<EasySalesServerUser> userManager;

        public ProductosController(IProductosRepository productosRepository, UserManager<EasySalesServerUser> userManager)
        {
            this.productosRepository = productosRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> CargarProductos()
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return Ok(await productosRepository.CargarProductos());
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
        [Route("ventas/")]
        public async Task<ActionResult> CargarProductosVentas()
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = await userManager.GetUserAsync(User);
                        return Ok(await productosRepository.CargarProductosVentas(user.EmpresaId));
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
        [Route("reservas/")]
        public async Task<ActionResult> CargarProductosReservas()
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return Ok(await productosRepository.CargarProductosReservas());
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

        [HttpGet("{IdProducto:int}")]
        public async Task<ActionResult<Productos>> ObtenerProductoXId(long IdProducto)
        {
            try
            {
                var Resultado = await productosRepository.ObtenerProductoXId(IdProducto);
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

        [HttpPost]
        public async Task<ActionResult<Productos>> AgregarProducto(Productos producto)
        {
            try
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (producto == null)
                            return BadRequest();

                        var prod = await productosRepository.ObtenerProductoXNombre(producto.Nombre);

                        if (prod != null)
                        {
                            ModelState.AddModelError("Nombre", "Ya existe un producto con ese nombre.");
                            return BadRequest(ModelState);
                        }

                        var user = await userManager.GetUserAsync(User);
                        producto.EmpresaId = user.EmpresaId;

                        var NuevoProducto = await productosRepository.AgregarProducto(producto);

                        return CreatedAtAction(nameof(ObtenerProductoXId),
                            new { IdProducto = NuevoProducto.Id }, NuevoProducto);
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

        [HttpPut("{IdProducto:int}")]
        public async Task<ActionResult<Productos>> ModificarProducto(int IdProducto, Productos producto)
        {
            try
            {
                if (IdProducto == null)
                {
                    return NotFound();
                }

                if (IdProducto != producto.Id)
                {
                    return BadRequest("Id del producto no es valido");
                }

                var prod = await productosRepository.ObtenerProductoXId(Convert.ToInt64(IdProducto));

                if (prod == null)
                {
                    return NotFound($"No existe un producto con el Id ={IdProducto}");
                }

                var prodModificado = await productosRepository.ModificarProducto(producto);

                return prodModificado;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error modificando la informacion del producto en la base de datos. Error: " + ex.Message);
            }
        }

        [HttpDelete("{IdProducto:int}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(int IdProducto)
        {
            try
            {
                var prod = await productosRepository.ObtenerProductoXId(IdProducto);

                if (prod == null)
                {
                    return NotFound($"No existe un producto con el Id ={IdProducto}");
                }

                await productosRepository.EliminarProducto(IdProducto);

                return Ok("Producto eliminado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando el producto de la base de datos. Error: " + ex.Message);
            }
        }

        //[Route("api/productos/buscar/{filtro}")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Productos>>> Search(string? Filtro, int? ObjCategoriaProducto)
        //{
        //    try
        //    {
        //        var resultado = await productosRepository.Buscar(Filtro, ObjCategoriaProducto);

        //        if (resultado.Any())
        //        {
        //            return Ok(resultado);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //        "Error obteniendo la información de la base de datos. Error: " + ex.Message);
        //    }
        //}

    }
}
