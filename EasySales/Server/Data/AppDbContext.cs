using EasySales.Shared;
using EasySales.Shared.Inventario;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasySales.Server.Data
{
    public class AppDbContext : IdentityDbContext<EasySalesServerUser, EasySalesServerRoles, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }

        public DbSet<Productos> Productos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public DbSet<CategoriaProductos> CategoriaProductos { get; set; }
        public DbSet<SubCategoriaProductos> SubCategoriaProductos { get; set; }
        public DbSet<DepartamentoEmpresa> DepartamentoEmpresa { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<PuestoLaboral> PuestoLaboral { get; set; }
        public DbSet<TipoModificacion> TipoModificacion { get; set; }
        public DbSet<RegimenEmpresarial> RegimenEmpresarial { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Sucursales> Sucursales { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<EstadosFactura> EstadosFactura { get; set; }
        public DbSet<ParametrosSistema> ParametrosSistema { get; set; }
        public DbSet<EasySalesServerUser> EasySalesServerUser { get; set; }
        public DbSet<EasySalesServerRoles> EasySalesServerRoles { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<TasaCambioOficial> TasaCambioOficial { get; set; }
        public DbSet<TasaCambioCompra> TasaCambioCompra { get; set; }
        public DbSet<TasaCambioVenta> TasaCambioVenta { get; set; }
        public DbSet<Cajas> Cajas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<Reservaciones> Reservaciones { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }
        public DbSet<Bodegas> Bodegas { get; set; }
        public DbSet<CantidadInventarioMatriz> CantidadInventarioMatriz { get; set; }
        public DbSet<BodegaSucursalMatriz> BodegaSucursalMatriz { get; set; }
        


        //metodo para agregar datos cuando se crean las tablas basado en los modelos en la migracion
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    ////Seed Departments Table
        //    //modelBuilder.Entity<Department>().HasData(
        //    //    new Department { DepartmentId = 1, DepartmentName = "IT" });
        //    //modelBuilder.Entity<Department>().HasData(
        //    //    new Department { DepartmentId = 2, DepartmentName = "HR" });
        //    //modelBuilder.Entity<Department>().HasData(
        //    //    new Department { DepartmentId = 3, DepartmentName = "Payroll" });
        //    //modelBuilder.Entity<Department>().HasData(
        //    //    new Department { DepartmentId = 4, DepartmentName = "Admin" });

        //    // Seed Productos Table
        //    modelBuilder.Entity<Productos>().HasData(new Productos
        //    {
        //        Id = 1,
        //        Nombre = "Producto 1",
        //        Descripcion = "Descripcion del producto",
        //        PrecioCosto = 12.90M,
        //        PrecioVenta = 20,
        //        EsServicio=true,
        //        AplicaIVA=false,
        //        Activo=true,
        //        FechaCreacion = DateTime.Now,
        //        UsuarioCreacion = "PruebaInicial",
        //        ObjCategoriaProducto=1,
        //        ObjSubCategoriaProductoId=0
        //    });

        //    modelBuilder.Entity<Productos>().HasData(new Productos
        //    {
        //        Id = 2,
        //        Nombre = "Producto 2",
        //        Descripcion = "Descripcion del producto",
        //        PrecioCosto = 25.90M,
        //        PrecioVenta = 50,
        //        EsServicio = true,
        //        AplicaIVA = false,
        //        Activo = true,
        //        FechaCreacion = DateTime.Now,
        //        UsuarioCreacion = "PruebaInicial",
        //        ObjCategoriaProducto = 1,
        //        ObjSubCategoriaProductoId = 0
        //    });

        //    modelBuilder.Entity<Productos>().HasData(new Productos
        //    {
        //        Id = 3,
        //        Nombre = "Producto 3",
        //        Descripcion = "Descripcion del producto",
        //        PrecioCosto = 45.35M,
        //        PrecioVenta = 70.80M,
        //        EsServicio = true,
        //        AplicaIVA = false,
        //        Activo = true,
        //        FechaCreacion = DateTime.Now,
        //        UsuarioCreacion = "PruebaInicial",
        //        ObjCategoriaProducto = 1,
        //        ObjSubCategoriaProductoId = 0
        //    });
        //}


    }
}
