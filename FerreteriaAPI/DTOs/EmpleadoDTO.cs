namespace FerreteriaAPI.DTOs
{
    public class EmpleadoDTO
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string DPI { get; set; } = string.Empty;
        public string? Puesto { get; set; }
        public decimal? Salario { get; set; }
        public bool Activo { get; set; }
    }

    public class EmpleadoCreacionDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string DPI { get; set; } = string.Empty;
        public string? Puesto { get; set; }
        public decimal? Salario { get; set; }
    }
}