using FerreteriaAPI.Data;
using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly FerreteriaDbContext _context;

        public ClienteService(FerreteriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDTO>> ObtenerTodosAsync()
        {
            var clientes = await _context.Clientes
                .Where(c => c.Activo)
                .Select(c => new ClienteDTO
                {
                    ClienteID = c.ClienteID,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    NIT = c.NIT,
                    Activo = c.Activo
                })
                .ToListAsync();

            return clientes;
        }

        public async Task<ClienteDTO?> ObtenerPorIdAsync(int id)
        {
            var cliente = await _context.Clientes
                .Where(c => c.ClienteID == id && c.Activo)
                .Select(c => new ClienteDTO
                {
                    ClienteID = c.ClienteID,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    NIT = c.NIT,
                    Activo = c.Activo
                })
                .FirstOrDefaultAsync();

            return cliente;
        }

        public async Task<ClienteDTO?> CrearAsync(ClienteCreacionDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,
                Direccion = clienteDTO.Direccion,
                Telefono = clienteDTO.Telefono,
                Email = clienteDTO.Email,
                NIT = clienteDTO.NIT,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteDTO
            {
                ClienteID = cliente.ClienteID,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                NIT = cliente.NIT,
                Activo = cliente.Activo
            };
        }

        public async Task<bool> ActualizarAsync(int id, ClienteCreacionDTO clienteDTO)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            cliente.Nombre = clienteDTO.Nombre;
            cliente.Apellido = clienteDTO.Apellido;
            cliente.Direccion = clienteDTO.Direccion;
            cliente.Telefono = clienteDTO.Telefono;
            cliente.Email = clienteDTO.Email;
            cliente.NIT = clienteDTO.NIT;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            // Eliminación lógica
            cliente.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}