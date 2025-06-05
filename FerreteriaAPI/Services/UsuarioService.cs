using FerreteriaAPI.Data;
using FerreteriaAPI.DTOs;
using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FerreteriaAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly FerreteriaDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(FerreteriaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<UsuarioDTO>> ObtenerTodosAsync()
        {
            var usuarios = await _context.Usuarios
                .Where(u => u.Activo)
                .Select(u => new UsuarioDTO
                {
                    UsuarioID = u.UsuarioID,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    NombreUsuario = u.NombreUsuario,
                    Activo = u.Activo
                })
                .ToListAsync();

            return usuarios;
        }

        public async Task<UsuarioDTO?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.UsuarioID == id && u.Activo)
                .Select(u => new UsuarioDTO
                {
                    UsuarioID = u.UsuarioID,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    NombreUsuario = u.NombreUsuario,
                    Activo = u.Activo
                })
                .FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<UsuarioRespuestaDTO?> CrearAsync(UsuarioCreacionDTO usuarioDTO)
        {
            // Verificar si el nombre de usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario))
            {
                return null;
            }

            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                NombreUsuario = usuarioDTO.NombreUsuario,
                Contrasena = usuarioDTO.Contrasena, // En producción, hashear la contraseña
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Generar token JWT
            var token = GenerarToken(usuario);

            return new UsuarioRespuestaDTO
            {
                UsuarioID = usuario.UsuarioID,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Token = token
            };
        }

        public async Task<bool> ActualizarAsync(int id, UsuarioCreacionDTO usuarioDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            // Verificar si el nombre de usuario ya existe y no es el mismo usuario
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario && u.UsuarioID != id))
            {
                return false;
            }

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Apellido = usuarioDTO.Apellido;
            usuario.NombreUsuario = usuarioDTO.NombreUsuario;

            // Solo actualizar la contraseña si se proporciona una nueva
            if (!string.IsNullOrEmpty(usuarioDTO.Contrasena))
            {
                usuario.Contrasena = usuarioDTO.Contrasena; // En producción, hashear la contraseña
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            // Eliminación lógica
            usuario.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UsuarioRespuestaDTO?> LoginAsync(UsuarioLoginDTO loginDTO)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == loginDTO.NombreUsuario && u.Activo);

            if (usuario == null || usuario.Contrasena != loginDTO.Contrasena) // En producción, verificar hash
            {
                return null;
            }

            // Generar token JWT
            var token = GenerarToken(usuario);

            return new UsuarioRespuestaDTO
            {
                UsuarioID = usuario.UsuarioID,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Token = token
            };
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.GivenName, $"{usuario.Nombre} {usuario.Apellido}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value ?? "clave_secreta_por_defecto_para_desarrollo"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}