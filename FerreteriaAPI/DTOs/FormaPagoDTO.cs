﻿namespace FerreteriaAPI.DTOs
{
    public class FormaPagoDTO
    {
        public int FormaPagoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class FormaPagoCreacionDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}