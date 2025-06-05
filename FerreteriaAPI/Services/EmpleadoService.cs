using FerreteriaAPI.Data;
using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaAPI.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly FerreteriaDbContext _context;

        public EmpleadoService(FerreteriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDTO>> ObtenerTodosAsync()
        {
            var empleados = await _context.Empleados
                .Where(e => e.Activo)
                .Select(e => new EmpleadoDTO
                {
                    EmpleadoID = e.EmpleadoID,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Direccion = e.Direccion,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    DPI = e.DPI,
                    Puesto = e.Puesto,
                    Salario = e.Salario,
                    Activo = e.Activo
                })
                .ToListAsync();

            return empleados;
        }

        public async Task<EmpleadoDTO?> ObtenerPorIdAsync(int id)
        {
            var empleado = await _context.Empleados
                .Where(e => e.EmpleadoID == id && e.Activo)
                .Select(e => new EmpleadoDTO
                {
                    EmpleadoID = e.EmpleadoID,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Direccion = e.Direccion,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    DPI = e.DPI,
                    Puesto = e.Puesto,
                    Salario = e.Salario,
                    Activo = e.Activo
                })
                .FirstOrDefaultAsync();

            return empleado;
        }

        public async Task<EmpleadoDTO?> CrearAsync(EmpleadoCreacionDTO empleadoDTO)
        {
            // Verificar si el DPI ya existe
            if (await _context.Empleados.AnyAsync(e => e.DPI == empleadoDTO.DPI))
            {
                return null;
            }

            var empleado = new Empleado
            {
                Nombre = empleadoDTO.Nombre,
                Apellido = empleadoDTO.Apellido,
                Direccion = empleadoDTO.Direccion,
                Telefono = empleadoDTO.Telefono,
                Email = empleadoDTO.Email,
                DPI = empleadoDTO.DPI,
                Puesto = empleadoDTO.Puesto,
                Salario = empleadoDTO.Salario,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return new EmpleadoDTO
            {
                EmpleadoID = empleado.EmpleadoID,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Direccion = empleado.Direccion,
                Telefono = empleado.Telefono,
                Email = empleado.Email,
                DPI = empleado.DPI,
                Puesto = empleado.Puesto,
                Salario = empleado.Salario,
                Activo = empleado.Activo
            };
        }

        public async Task<bool> ActualizarAsync(int id, EmpleadoCreacionDTO empleadoDTO)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return false;
            }

            // Verificar si el DPI ya existe y no es el mismo empleado
            if (await _context.Empleados.AnyAsync(e => e.DPI == empleadoDTO.DPI && e.EmpleadoID != id))
            {
                return false;
            }

            empleado.Nombre = empleadoDTO.Nombre;
            empleado.Apellido = empleadoDTO.Apellido;
            empleado.Direccion = empleadoDTO.Direccion;
            empleado.Telefono = empleadoDTO.Telefono;
            empleado.Email = empleadoDTO.Email;
            empleado.DPI = empleadoDTO.DPI;
            empleado.Puesto = empleadoDTO.Puesto;
            empleado.Salario = empleadoDTO.Salario;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return false;
            }

            // Eliminación lógica
            empleado.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}