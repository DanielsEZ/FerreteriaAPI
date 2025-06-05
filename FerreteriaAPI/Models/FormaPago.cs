﻿namespace FerreteriaAPI.Models
{
    public class FormaPago
    {
        public int FormaPagoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}