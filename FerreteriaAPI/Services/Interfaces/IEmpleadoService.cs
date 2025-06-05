using FerreteriaAPI.DTOs;

namespace FerreteriaAPI.Services
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> ObtenerTodosAsync();
        Task<EmpleadoDTO?> ObtenerPorIdAsync(int id);
        Task<EmpleadoDTO?> CrearAsync(EmpleadoCreacionDTO empleadoDTO);
        Task<bool> ActualizarAsync(int id, EmpleadoCreacionDTO empleadoDTO);
        Task<bool> EliminarAsync(int id);
    }
}