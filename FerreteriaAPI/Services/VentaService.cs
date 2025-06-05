using FerreteriaAPI.Data;
using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaAPI.Services
{
    public class VentaService : IVentaService
    {
        private readonly FerreteriaDbContext _context;
        private readonly IArticuloService _articuloService;

        public VentaService(FerreteriaDbContext context, IArticuloService articuloService)
        {
            _context = context;
            _articuloService = articuloService;
        }

        public async Task<List<VentaDTO>> ObtenerTodasAsync()
        {
            // Modificación: Separar la consulta para evitar problemas con ThenInclude
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.FormaPago)
                .Include(v => v.DetallesVenta)
                .ToListAsync();

            // Cargar los artículos para cada detalle
            foreach (var venta in ventas)
            {
                foreach (var detalle in venta.DetallesVenta!)
                {
                    await _context.Entry(detalle)
                        .Reference(d => d.Articulo)
                        .LoadAsync();
                }
            }

            // Mapear a DTOs
            return ventas.Select(v => new VentaDTO
            {
                VentaID = v.VentaID,
                ClienteID = v.ClienteID,
                NombreCliente = $"{v.Cliente!.Nombre} {v.Cliente.Apellido}",
                EmpleadoID = v.EmpleadoID,
                NombreEmpleado = $"{v.Empleado!.Nombre} {v.Empleado.Apellido}",
                FormaPagoID = v.FormaPagoID,
                FormaPagoNombre = v.FormaPago!.Nombre,
                FechaVenta = v.FechaVenta,
                Subtotal = v.Subtotal,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado,
                DetallesVenta = v.DetallesVenta!.Select(d => new DetalleVentaDTO
                {
                    DetalleVentaID = d.DetalleVentaID,
                    ArticuloID = d.ArticuloID,
                    NombreArticulo = d.Articulo!.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList();
        }

        public async Task<VentaDTO?> ObtenerPorIdAsync(int id)
        {
            // Modificación: Usar FirstOrDefault antes de proyectar a DTO
            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.FormaPago)
                .Include(v => v.DetallesVenta)
                .FirstOrDefaultAsync(v => v.VentaID == id);

            if (venta == null)
                return null;

            // Cargar los artículos para cada detalle
            foreach (var detalle in venta.DetallesVenta!)
            {
                await _context.Entry(detalle)
                    .Reference(d => d.Articulo)
                    .LoadAsync();
            }

            // Mapear a DTO
            return new VentaDTO
            {
                VentaID = venta.VentaID,
                ClienteID = venta.ClienteID,
                NombreCliente = $"{venta.Cliente!.Nombre} {venta.Cliente.Apellido}",
                EmpleadoID = venta.EmpleadoID,
                NombreEmpleado = $"{venta.Empleado!.Nombre} {venta.Empleado.Apellido}",
                FormaPagoID = venta.FormaPagoID,
                FormaPagoNombre = venta.FormaPago!.Nombre,
                FechaVenta = venta.FechaVenta,
                Subtotal = venta.Subtotal,
                Impuesto = venta.Impuesto,
                Total = venta.Total,
                Estado = venta.Estado,
                DetallesVenta = venta.DetallesVenta!.Select(d => new DetalleVentaDTO
                {
                    DetalleVentaID = d.DetalleVentaID,
                    ArticuloID = d.ArticuloID,
                    NombreArticulo = d.Articulo!.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }

        public async Task<List<VentaDTO>> ObtenerPorClienteAsync(int clienteId)
        {
            // Modificación: Separar la consulta para evitar problemas con ThenInclude
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.FormaPago)
                .Include(v => v.DetallesVenta)
                .Where(v => v.ClienteID == clienteId)
                .ToListAsync();

            // Cargar los artículos para cada detalle
            foreach (var venta in ventas)
            {
                foreach (var detalle in venta.DetallesVenta!)
                {
                    await _context.Entry(detalle)
                        .Reference(d => d.Articulo)
                        .LoadAsync();
                }
            }

            // Mapear a DTOs
            return ventas.Select(v => new VentaDTO
            {
                VentaID = v.VentaID,
                ClienteID = v.ClienteID,
                NombreCliente = $"{v.Cliente!.Nombre} {v.Cliente.Apellido}",
                EmpleadoID = v.EmpleadoID,
                NombreEmpleado = $"{v.Empleado!.Nombre} {v.Empleado.Apellido}",
                FormaPagoID = v.FormaPagoID,
                FormaPagoNombre = v.FormaPago!.Nombre,
                FechaVenta = v.FechaVenta,
                Subtotal = v.Subtotal,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado,
                DetallesVenta = v.DetallesVenta!.Select(d => new DetalleVentaDTO
                {
                    DetalleVentaID = d.DetalleVentaID,
                    ArticuloID = d.ArticuloID,
                    NombreArticulo = d.Articulo!.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList();
        }

        public async Task<List<VentaDTO>> ObtenerPorFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            // Ajustar la fecha final para incluir todo el día
            fechaFin = fechaFin.AddDays(1).AddTicks(-1);

            // Modificación: Separar la consulta para evitar problemas con ThenInclude
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.FormaPago)
                .Include(v => v.DetallesVenta)
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                .ToListAsync();

            // Cargar los artículos para cada detalle
            foreach (var venta in ventas)
            {
                foreach (var detalle in venta.DetallesVenta!)
                {
                    await _context.Entry(detalle)
                        .Reference(d => d.Articulo)
                        .LoadAsync();
                }
            }

            // Mapear a DTOs
            return ventas.Select(v => new VentaDTO
            {
                VentaID = v.VentaID,
                ClienteID = v.ClienteID,
                NombreCliente = $"{v.Cliente!.Nombre} {v.Cliente.Apellido}",
                EmpleadoID = v.EmpleadoID,
                NombreEmpleado = $"{v.Empleado!.Nombre} {v.Empleado.Apellido}",
                FormaPagoID = v.FormaPagoID,
                FormaPagoNombre = v.FormaPago!.Nombre,
                FechaVenta = v.FechaVenta,
                Subtotal = v.Subtotal,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado,
                DetallesVenta = v.DetallesVenta!.Select(d => new DetalleVentaDTO
                {
                    DetalleVentaID = d.DetalleVentaID,
                    ArticuloID = d.ArticuloID,
                    NombreArticulo = d.Articulo!.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList();
        }

        public async Task<VentaDTO?> CrearAsync(VentaCreacionDTO ventaDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Verificar que existan el cliente, empleado y forma de pago
                var cliente = await _context.Clientes.FindAsync(ventaDTO.ClienteID);
                var empleado = await _context.Empleados.FindAsync(ventaDTO.EmpleadoID);
                var formaPago = await _context.FormasPago.FindAsync(ventaDTO.FormaPagoID);

                if (cliente == null || empleado == null || formaPago == null)
                {
                    return null;
                }

                // Verificar que haya detalles de venta
                if (ventaDTO.DetallesVenta.Count == 0)
                {
                    return null;
                }

                decimal subtotal = 0;
                var detalles = new List<DetalleVenta>();

                // Procesar cada detalle de venta
                foreach (var detalleDTO in ventaDTO.DetallesVenta)
                {
                    var articulo = await _context.Articulos.FindAsync(detalleDTO.ArticuloID);
                    if (articulo == null || !articulo.Activo || articulo.Stock < detalleDTO.Cantidad)
                    {
                        return null; // Artículo no existe, no está activo o no hay suficiente stock
                    }

                    // Actualizar stock
                    articulo.Stock -= detalleDTO.Cantidad;

                    // Calcular subtotal del detalle
                    decimal subtotalDetalle = articulo.PrecioVenta * detalleDTO.Cantidad;
                    subtotal += subtotalDetalle;

                    // Crear detalle de venta
                    var detalle = new DetalleVenta
                    {
                        ArticuloID = detalleDTO.ArticuloID,
                        Cantidad = detalleDTO.Cantidad,
                        PrecioUnitario = articulo.PrecioVenta,
                        Subtotal = subtotalDetalle
                    };

                    detalles.Add(detalle);
                }

                // Calcular impuesto (12% por ejemplo)
                decimal impuesto = subtotal * 0.12m;
                decimal total = subtotal + impuesto;

                // Crear venta
                var venta = new Venta
                {
                    ClienteID = ventaDTO.ClienteID,
                    EmpleadoID = ventaDTO.EmpleadoID,
                    FormaPagoID = ventaDTO.FormaPagoID,
                    FechaVenta = DateTime.Now,
                    Subtotal = subtotal,
                    Impuesto = impuesto,
                    Total = total,
                    Estado = "Completada",
                    DetallesVenta = detalles
                };

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Retornar la venta creada
                return await ObtenerPorIdAsync(venta.VentaID);
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<bool> ActualizarEstadoAsync(int id, string estado)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return false;
            }

            venta.Estado = estado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}