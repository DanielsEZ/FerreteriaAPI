using FerreteriaAPI.DTOs;

namespace FerreteriaAPI.Services
{
    public interface IArticuloService
    {
        Task<List<ArticuloDTO>> ObtenerTodosAsync();
        Task<ArticuloDTO?> ObtenerPorIdAsync(int id);
        Task<ArticuloDTO?> ObtenerPorCodigoAsync(string codigo);
        Task<ArticuloDTO?> CrearAsync(ArticuloCreacionDTO articuloDTO);
        Task<bool> ActualizarAsync(int id, ArticuloCreacionDTO articuloDTO);
        Task<bool> ActualizarStockAsync(int id, int cantidad);
        Task<bool> EliminarAsync(int id);
    }
}