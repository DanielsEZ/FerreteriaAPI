using FerreteriaAPI.DTOs;

namespace FerreteriaAPI.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ObtenerTodosAsync();
        Task<ClienteDTO?> ObtenerPorIdAsync(int id);
        Task<ClienteDTO?> CrearAsync(ClienteCreacionDTO clienteDTO);
        Task<bool> ActualizarAsync(int id, ClienteCreacionDTO clienteDTO);
        Task<bool> EliminarAsync(int id);
    }
}