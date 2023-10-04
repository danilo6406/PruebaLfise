using AutoMapper;
using EasySales.Server.Data;
using EasySales.Shared;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace EasySales.Server.Models
{
    public class ReservacionesRepository : IReservacionesRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public ReservacionesRepository(AppDbContext appDbContext, IConfiguration configuration, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<Reservaciones> Agregar(Reservaciones claseEntrante)
        {
            try
            {
                claseEntrante.Producto = null;
                claseEntrante.Cliente = null;
                var estadoReserva = await appDbContext.EstadoReserva.FirstOrDefaultAsync(e => e.Nombre == "Pendiente");
                claseEntrante.EstadoReservaId = estadoReserva.Id;

                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "INSERT");
                claseEntrante.TipoModificacionId = tipoModificacion.Id;

                var resultado = await appDbContext.Reservaciones.AddAsync(claseEntrante);
                await appDbContext.SaveChangesAsync();
                return resultado.Entity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Reservaciones>> CargarDatos()
        {
            try
            {
                var reservaciones = await appDbContext.Reservaciones.Include(e => e.Cliente).Include(e => e.Producto).Where(w => w.EstadoReservaId != 2).OrderByDescending(e => e.FechaReservacion).ToListAsync();
                return reservaciones;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Anular(long Id)
        {
            try
            {
                var estadoreserva = await appDbContext.EstadoReserva.FirstOrDefaultAsync(e => e.Nombre == "Anulada");
                var reservacion = await appDbContext.Reservaciones
                   .FirstOrDefaultAsync(e => e.Id == Id);

                reservacion.EstadoReservaId = estadoreserva.Id;
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Reservaciones> Modificar(Reservaciones claseEntrante)
        {
            try
            {
                var tipoModificacion = await appDbContext.TipoModificacion.FirstOrDefaultAsync(e => e.CodigoInterno == "Edit");
                var result = await appDbContext.Reservaciones.FirstOrDefaultAsync(e => e.Id == claseEntrante.Id);

                if (result != null)
                {
                    claseEntrante.FechaModificacion = DateTime.Now;
                    claseEntrante.Cliente = null;
                    claseEntrante.Producto = null;
                    claseEntrante.TipoModificacion = null;
                    claseEntrante.EstadoReserva = null;
                    //claseEntrante.TipoModificacionId = tipoModificacion.Id;                    
                    //mapper.Map(claseEntrante, result);

                    result.ClienteId = claseEntrante.ClienteId;
                    result.ProductoId = claseEntrante.ProductoId;
                    result.FechaReservacion = claseEntrante.FechaReservacion;
                    result.MontoReservacion = claseEntrante.MontoReservacion;
                    result.CantidadHoras = claseEntrante.CantidadHoras;
                    result.HoraInicio = claseEntrante.HoraInicio;
                    result.HoraFin = claseEntrante.HoraFin;
                    result.Observacion = claseEntrante.Observacion;
                    result.Titulo = claseEntrante.Titulo;
                    result.FechaModificacion = claseEntrante.FechaModificacion;
                    result.UsuarioModificacion = claseEntrante.UsuarioModificacion;
                    result.TipoModificacionId = tipoModificacion.Id;


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

        public async Task<Reservaciones> ObtenerXId(long Id)
        {
            try
            {
                return await appDbContext.Reservaciones.Include(e => e.Cliente).Include(e => e.Producto).FirstOrDefaultAsync(e => e.Id == Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<long> ComprobarDisponibilidad(Reservaciones claseEntrante)
        {
            try
            {
                string sp = ProcedimientosAlmacenadosSql.Instance.SpComprobarDisponibilidadReservacion;
                using (MySqlConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    using (MySqlCommand command = new MySqlCommand(sp, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter to the command
                        command.Parameters.AddWithValue("pFechaReservacion", claseEntrante.FechaReservacion.Date);
                        command.Parameters.AddWithValue("pHoraInicio", claseEntrante.HoraInicio.TimeOfDay);
                        command.Parameters.AddWithValue("pHoraFin", claseEntrante.HoraFin.TimeOfDay);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable data = new();
                            // Fill the DataSet with data from the stored procedure
                            adapter.Fill(data);
                            return Convert.ToInt64(data.Rows[0]["ReservacionEnConflicto"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
