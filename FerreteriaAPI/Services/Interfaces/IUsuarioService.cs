using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;

namespace FerreteriaAPI.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> ObtenerTodosAsync();
        Task<UsuarioDTO?> ObtenerPorIdAsync(int id);
        Task<UsuarioRespuestaDTO?> CrearAsync(UsuarioCreacionDTO usuarioDTO);
        Task<bool> ActualizarAsync(int id, UsuarioCreacionDTO usuarioDTO);
        Task<bool> EliminarAsync(int id);
        Task<UsuarioRespuestaDTO?> LoginAsync(UsuarioLoginDTO loginDTO);
    }
}