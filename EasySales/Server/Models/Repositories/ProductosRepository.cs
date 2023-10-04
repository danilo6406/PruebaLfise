using EasySales.Server.Data;
using EasySales.Shared;
using EasySales.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace EasySales.Server.Models
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public ProductosRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<Productos> AgregarProducto(Productos Producto)
        {
            //Cuando se guarda un producto con una propiedad con clase pero no se quiere actualizar la tabla de la clase.
            //if (Producto.Department != null)
            //{
            //    appDbContext.Entry(Producto.Department).State = EntityState.Unchanged;
            //}
            try
            {
                var resultado = await appDbContext.Productos.AddAsync(Producto);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Productos>> Buscar(string? Filtro, int? IdCategoriaProducto)
        {
            IQueryable<Productos> query = appDbContext.Productos;

            if (!string.IsNullOrEmpty(Filtro))
            {
                query = query.Where(e => e.Nombre.Contains(Filtro) || e.Descripcion.Contains(Filtro));
            }

            if (IdCategoriaProducto != null)
            {
                query = query.Where(e => e.CategoriaProductos.Id == (IdCategoriaProducto));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Productos>> CargarProductos()
        {
            try
            {
                var productos = await appDbContext.Productos.Include(e => e.CategoriaProductos).Include(e => e.SubCategoriaProductos).ToListAsync();
                return productos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductosDisponibles>> CargarProductosVentas(int EmpresaId)
        {
            try
            {
                string sp = ProcedimientosAlmacenadosSql.Instance.SpListaProductosGet;
                List<ProductosDisponibles> productosList = new();
                using (MySqlConnection connection = new(configuration.GetConnectionString("DBConnection")))
                {
                    using (MySqlCommand command = new(sp, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter to the command
                        command.Parameters.AddWithValue("EmpresaId", EmpresaId);

                        #region Opcion llenar datatable

                        //        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        //        {
                        //            DataSet dataSet = new DataSet();

                        //            // Fill the DataSet with data from the stored procedure
                        //            adapter.Fill(dataSet);

                        //            // Process the data in the DataSet as needed
                        //            DataTable dataTable = dataSet.Tables[0];
                        //            foreach (DataRow row in dataTable.Rows)
                        //            {
                        //                // Access the data in each row
                        //                string customerName = row["customer_name"].ToString();
                        //                string country = row["country"].ToString();

                        //                Console.WriteLine($"Customer: {customerName}, Country: {country}");
                        //            }
                        //        }

                        #endregion

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductosDisponibles producto = new ProductosDisponibles
                                {
                                    Id = reader.GetInt64("Id"),
                                    Empresa = null,
                                    EmpresaId = reader.GetInt32("EmpresaId"),
                                    Nombre = reader.GetString("Nombre"),
                                    Descripcion = reader.GetString("Descripcion"),
                                    PrecioCosto = reader.GetDecimal("PrecioCosto"),
                                    PrecioVenta = reader.GetDecimal("PrecioVenta"),
                                    EsServicio = reader.GetBoolean("EsServicio"),
                                    AplicaIVA = reader.GetBoolean("AplicaIVA"),
                                    Activo = reader.GetBoolean("Activo"),
                                    CategoriaProductos = null,
                                    CategoriaProductosId = reader.GetInt64("CategoriaProductosId"),
                                    SubCategoriaProductos = null,
                                    SubCategoriaProductosId = reader.GetInt64("SubCategoriaProductosId"),
                                    FotoPath = reader.GetString("FotoPath"),
                                    UsuarioCreacion = "Sistema",
                                    FechaCreacion = DateTime.Now,
                                    UsuarioModificacion = "",
                                    FechaModificacion = DateTime.Now,
                                    TipoModificacion = null,
                                    TipoModificacionId = 1,
                                    Cantidad = reader.GetInt32("Cantidad")
                                };
                                productosList.Add(producto);
                            }
                        }

                        return productosList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Productos>> CargarProductosReservas()
        {
            try
            {
                var productos = await appDbContext.Productos.Where(e => e.EsServicio == true).Include(e => e.CategoriaProductos).Include(e => e.SubCategoriaProductos).ToListAsync();
                return productos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task EliminarProducto(long ProductoId)
        {
            var result = await appDbContext.Productos
               .FirstOrDefaultAsync(e => e.Id == ProductoId);

            if (result != null)
            {
                appDbContext.Productos.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Productos> ModificarProducto(Productos Producto)
        {
            try
            {
                var result = await appDbContext.Productos.FirstOrDefaultAsync(e => e.Id == Producto.Id);
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");
                var categoriaProducto = await appDbContext.CategoriaProductos.FirstOrDefaultAsync(e => e.Id == Producto.CategoriaProductos.Id);
                SubCategoriaProductos subCategoriaProducto = new();
                if (Producto.SubCategoriaProductos.Id != 0)
                {
                    subCategoriaProducto = await appDbContext.SubCategoriaProductos.FirstOrDefaultAsync(e => e.Id == Producto.SubCategoriaProductos.Id);
                }
                else
                {
                    subCategoriaProducto = await appDbContext.SubCategoriaProductos.FirstOrDefaultAsync(e => e.Id == Producto.SubCategoriaProductosId);
                }

                if (result != null)
                {
                    result.Nombre = Producto.Nombre;
                    result.Descripcion = Producto.Descripcion;
                    result.PrecioVenta = Producto.PrecioVenta;
                    result.PrecioCosto = Producto.PrecioCosto;
                    result.AplicaIVA = Producto.AplicaIVA;
                    result.Activo = Producto.Activo;
                    result.EsServicio = Producto.EsServicio;
                    result.FechaModificacion = DateTime.Now;
                    result.UsuarioModificacion = Producto.UsuarioModificacion;
                    result.CategoriaProductos = categoriaProducto;
                    result.CategoriaProductosId = categoriaProducto.Id;
                    result.SubCategoriaProductos = subCategoriaProducto;
                    result.SubCategoriaProductosId = subCategoriaProducto.Id;
                    result.TipoModificacion = tipoModificacion;
                    result.TipoModificacionId = tipoModificacion.Id;
                    result.FotoPath = Producto.FotoPath;

                    await appDbContext.SaveChangesAsync();

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Productos> ObtenerProductoXId(long IdProducto)
        {
            try
            {
                return await appDbContext.Productos.Include(e => e.CategoriaProductos).Include(e => e.SubCategoriaProductos).FirstOrDefaultAsync(e => e.Id == IdProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Productos> ObtenerProductoXCategoriaProducto(int IdCategoriaProducto)
        {
            return await appDbContext.Productos.FirstOrDefaultAsync(e => e.CategoriaProductos.Id == IdCategoriaProducto);
        }

        public async Task<Productos> ObtenerProductoXNombre(string Nombre)
        {
            return await appDbContext.Productos.FirstOrDefaultAsync(e => e.Nombre == Nombre);
        }
    }
}
