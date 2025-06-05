namespace FerreteriaAPI.DTOs
{
    public class ArticuloDTO
    {
        public int ArticuloID { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public string? Categoria { get; set; }
        public string? Proveedor { get; set; }
        public string? Ubicacion { get; set; }
        public bool Activo { get; set; }
    }

    public class ArticuloCreacionDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public string? Categoria { get; set; }
        public string? Proveedor { get; set; }
        public string? Ubicacion { get; set; }
    }
}