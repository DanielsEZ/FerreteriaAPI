using FerreteriaAPI.Data;
using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaAPI.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly FerreteriaDbContext _context;

        public ArticuloService(FerreteriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArticuloDTO>> ObtenerTodosAsync()
        {
            var articulos = await _context.Articulos
                .Where(a => a.Activo)
                .Select(a => new ArticuloDTO
                {
                    ArticuloID = a.ArticuloID,
                    Codigo = a.Codigo,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    PrecioCompra = a.PrecioCompra,
                    PrecioVenta = a.PrecioVenta,
                    Stock = a.Stock,
                    Categoria = a.Categoria,
                    Proveedor = a.Proveedor,
                    Ubicacion = a.Ubicacion,
                    Activo = a.Activo
                })
                .ToListAsync();

            return articulos;
        }

        public async Task<ArticuloDTO?> ObtenerPorIdAsync(int id)
        {
            var articulo = await _context.Articulos
                .Where(a => a.ArticuloID == id && a.Activo)
                .Select(a => new ArticuloDTO
                {
                    ArticuloID = a.ArticuloID,
                    Codigo = a.Codigo,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    PrecioCompra = a.PrecioCompra,
                    PrecioVenta = a.PrecioVenta,
                    Stock = a.Stock,
                    Categoria = a.Categoria,
                    Proveedor = a.Proveedor,
                    Ubicacion = a.Ubicacion,
                    Activo = a.Activo
                })
                .FirstOrDefaultAsync();

            return articulo;
        }

        public async Task<ArticuloDTO?> ObtenerPorCodigoAsync(string codigo)
        {
            var articulo = await _context.Articulos
                .Where(a => a.Codigo == codigo && a.Activo)
                .Select(a => new ArticuloDTO
                {
                    ArticuloID = a.ArticuloID,
                    Codigo = a.Codigo,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion,
                    PrecioCompra = a.PrecioCompra,
                    PrecioVenta = a.PrecioVenta,
                    Stock = a.Stock,
                    Categoria = a.Categoria,
                    Proveedor = a.Proveedor,
                    Ubicacion = a.Ubicacion,
                    Activo = a.Activo
                })
                .FirstOrDefaultAsync();

            return articulo;
        }

        public async Task<ArticuloDTO?> CrearAsync(ArticuloCreacionDTO articuloDTO)
        {
            // Verificar si el código ya existe
            if (await _context.Articulos.AnyAsync(a => a.Codigo == articuloDTO.Codigo))
            {
                return null;
            }

            var articulo = new Articulo
            {
                Codigo = articuloDTO.Codigo,
                Nombre = articuloDTO.Nombre,
                Descripcion = articuloDTO.Descripcion,
                PrecioCompra = articuloDTO.PrecioCompra,
                PrecioVenta = articuloDTO.PrecioVenta,
                Stock = articuloDTO.Stock,
                Categoria = articuloDTO.Categoria,
                Proveedor = articuloDTO.Proveedor,
                Ubicacion = articuloDTO.Ubicacion,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            return new ArticuloDTO
            {
                ArticuloID = articulo.ArticuloID,
                Codigo = articulo.Codigo,
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                PrecioCompra = articulo.PrecioCompra,
                PrecioVenta = articulo.PrecioVenta,
                Stock = articulo.Stock,
                Categoria = articulo.Categoria,
                Proveedor = articulo.Proveedor,
                Ubicacion = articulo.Ubicacion,
                Activo = articulo.Activo
            };
        }

        public async Task<bool> ActualizarAsync(int id, ArticuloCreacionDTO articuloDTO)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return false;
            }

            // Verificar si el código ya existe y no es el mismo artículo
            if (await _context.Articulos.AnyAsync(a => a.Codigo == articuloDTO.Codigo && a.ArticuloID != id))
            {
                return false;
            }

            articulo.Codigo = articuloDTO.Codigo;
            articulo.Nombre = articuloDTO.Nombre;
            articulo.Descripcion = articuloDTO.Descripcion;
            articulo.PrecioCompra = articuloDTO.PrecioCompra;
            articulo.PrecioVenta = articuloDTO.PrecioVenta;
            articulo.Stock = articuloDTO.Stock;
            articulo.Categoria = articuloDTO.Categoria;
            articulo.Proveedor = articuloDTO.Proveedor;
            articulo.Ubicacion = articuloDTO.Ubicacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarStockAsync(int id, int cantidad)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return false;
            }

            articulo.Stock += cantidad;
            if (articulo.Stock < 0)
            {
                return false; // No permitir stock negativo
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return false;
            }

            // Eliminación lógica
            articulo.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}