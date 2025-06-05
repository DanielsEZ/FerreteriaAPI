using FerreteriaAPI.DTOs;

namespace FerreteriaAPI.Services
{
    public interface IFormaPagoService
    {
        Task<List<FormaPagoDTO>> ObtenerTodasAsync();
        Task<FormaPagoDTO?> ObtenerPorIdAsync(int id);
        Task<FormaPagoDTO?> CrearAsync(FormaPagoCreacionDTO formaPagoDTO);
        Task<bool> ActualizarAsync(int id, FormaPagoCreacionDTO formaPagoDTO);
        Task<bool> EliminarAsync(int id);
    }
}