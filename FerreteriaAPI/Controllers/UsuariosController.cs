using FerreteriaAPI.DTOs;
using FerreteriaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioRespuestaDTO>> PostUsuario(UsuarioCreacionDTO usuarioDTO)
        {
            var usuario = await _usuarioService.CrearAsync(usuarioDTO);
            if (usuario == null)
            {
                return BadRequest("El nombre de usuario ya existe.");
            }
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuarioID }, usuario);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuario(int id, UsuarioCreacionDTO usuarioDTO)
        {
            var resultado = await _usuarioService.ActualizarAsync(id, usuarioDTO);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var resultado = await _usuarioService.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioRespuestaDTO>> Login(UsuarioLoginDTO loginDTO)
        {
            var usuario = await _usuarioService.LoginAsync(loginDTO);
            if (usuario == null)
            {
                return Unauthorized();
            }
            return Ok(usuario);
        }
    }
}