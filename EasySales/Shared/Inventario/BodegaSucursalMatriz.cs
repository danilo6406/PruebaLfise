using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySales.Shared.Inventario
{
    public class BodegaSucursalMatriz
    {
        public int Id { get; set; }
        public Empresa Empresa { get; set; }
        public Bodegas Bodega { get; set; }
        public Sucursales Sucursal { get; set; }

    }
}
