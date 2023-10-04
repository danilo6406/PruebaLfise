using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared
{
    public class ProcedimientosAlmacenadosSql:Object
    {
        private static ProcedimientosAlmacenadosSql instance;
        public static ProcedimientosAlmacenadosSql Instance => instance ??= new ProcedimientosAlmacenadosSql();

        public string SpListaProductosGet
        {
            get { return "SP_ListaProductos_Get"; }
        }

        public string SpComprobarDisponibilidadReservacion
        {
            get { return "SP_VerificarDisponibilidadReservaciones"; }
        }

        public string SPRestarProductoInventario
        {
            get { return "SP_RestarProductoInventario"; }
        }


        
    }
}
