using System.ComponentModel.DataAnnotations;

namespace EasySales.Shared
{
    public class CantidadInventarioMatriz
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public long ProductoId { get; set; }

        [Required]
        public int BodegaId { get; set;}

        [Required]
        public int Cantidad { get; }
    }
}
