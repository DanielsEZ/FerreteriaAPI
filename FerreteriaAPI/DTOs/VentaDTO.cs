namespace FerreteriaAPI.DTOs
{
    public class VentaDTO
    {
        public int VentaID { get; set; }
        public int ClienteID { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int EmpleadoID { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public int FormaPagoID { get; set; }
        public string FormaPagoNombre { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<DetalleVentaDTO> DetallesVenta { get; set; } = new List<DetalleVentaDTO>();
    }

    public class VentaCreacionDTO
    {
        public int ClienteID { get; set; }
        public int EmpleadoID { get; set; }
        public int FormaPagoID { get; set; }
        public List<DetalleVentaCreacionDTO> DetallesVenta { get; set; } = new List<DetalleVentaCreacionDTO>();
    }

    public class DetalleVentaDTO
    {
        public int DetalleVentaID { get; set; }
        public int ArticuloID { get; set; }
        public string NombreArticulo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class DetalleVentaCreacionDTO
    {
        public int ArticuloID { get; set; }
        public int Cantidad { get; set; }
    }
}