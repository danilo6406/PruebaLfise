using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared
{
    public class CantidadProductoInventario
    {
        public long ProductoId { get; set; }

        public int EmpresaId { get; set; }

        public int BodegaId { get; set; }

        public int Cantidad { get; set; }
    }
}
