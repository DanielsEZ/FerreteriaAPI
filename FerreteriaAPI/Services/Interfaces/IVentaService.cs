using FerreteriaAPI.DTOs;

namespace FerreteriaAPI.Services
{
    public interface IVentaService
    {
        Task<List<VentaDTO>> ObtenerTodasAsync();
        Task<VentaDTO?> ObtenerPorIdAsync(int id);
        Task<List<VentaDTO>> ObtenerPorClienteAsync(int clienteId);
        Task<List<VentaDTO>> ObtenerPorFechasAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<VentaDTO?> CrearAsync(VentaCreacionDTO ventaDTO);
        Task<bool> ActualizarEstadoAsync(int id, string estado);
    }
}