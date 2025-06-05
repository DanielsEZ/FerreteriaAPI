﻿namespace FerreteriaAPI.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? NIT { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}