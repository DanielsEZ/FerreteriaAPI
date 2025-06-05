namespace FerreteriaAPI.Models
{
    public class Venta
    {
        public int VentaID { get; set; }
        public int ClienteID { get; set; }
        public int EmpleadoID { get; set; }
        public int FormaPagoID { get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Completada";

        // Propiedades de navegación
        public Cliente? Cliente { get; set; }
        public Empleado? Empleado { get; set; }
        public FormaPago? FormaPago { get; set; }
        public List<DetalleVenta>? DetallesVenta { get; set; }
    }
}