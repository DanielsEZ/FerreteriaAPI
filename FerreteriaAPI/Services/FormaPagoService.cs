using FerreteriaAPI.Data;
using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaAPI.Services
{
    public class FormaPagoService : IFormaPagoService
    {
        private readonly FerreteriaDbContext _context;

        public FormaPagoService(FerreteriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<FormaPagoDTO>> ObtenerTodasAsync()
        {
            var formasPago = await _context.FormasPago
                .Where(f => f.Activo)
                .Select(f => new FormaPagoDTO
                {
                    FormaPagoID = f.FormaPagoID,
                    Nombre = f.Nombre,
                    Descripcion = f.Descripcion,
                    Activo = f.Activo
                })
                .ToListAsync();

            return formasPago;
        }

        public async Task<FormaPagoDTO?> ObtenerPorIdAsync(int id)
        {
            var formaPago = await _context.FormasPago
                .Where(f => f.FormaPagoID == id && f.Activo)
                .Select(f => new FormaPagoDTO
                {
                    FormaPagoID = f.FormaPagoID,
                    Nombre = f.Nombre,
                    Descripcion = f.Descripcion,
                    Activo = f.Activo
                })
                .FirstOrDefaultAsync();

            return formaPago;
        }

        public async Task<FormaPagoDTO?> CrearAsync(FormaPagoCreacionDTO formaPagoDTO)
        {
            var formaPago = new FormaPago
            {
                Nombre = formaPagoDTO.Nombre,
                Descripcion = formaPagoDTO.Descripcion,
                Activo = true
            };

            _context.FormasPago.Add(formaPago);
            await _context.SaveChangesAsync();

            return new FormaPagoDTO
            {
                FormaPagoID = formaPago.FormaPagoID,
                Nombre = formaPago.Nombre,
                Descripcion = formaPago.Descripcion,
                Activo = formaPago.Activo
            };
        }

        public async Task<bool> ActualizarAsync(int id, FormaPagoCreacionDTO formaPagoDTO)
        {
            var formaPago = await _context.FormasPago.FindAsync(id);
            if (formaPago == null)
            {
                return false;
            }

            formaPago.Nombre = formaPagoDTO.Nombre;
            formaPago.Descripcion = formaPagoDTO.Descripcion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var formaPago = await _context.FormasPago.FindAsync(id);
            if (formaPago == null)
            {
                return false;
            }

            // Eliminación lógica
            formaPago.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}