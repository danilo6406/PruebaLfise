namespace EasySales.Shared.ViewModels
{
    public class VentaProductos :Productos
    {
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalDetalle { get; set; }

    }
}
