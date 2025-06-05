using FerreteriaAPI.DTOs;
using FerreteriaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticulosController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloDTO>>> GetArticulos()
        {
            var articulos = await _articuloService.ObtenerTodosAsync();
            return Ok(articulos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloDTO>> GetArticulo(int id)
        {
            var articulo = await _articuloService.ObtenerPorIdAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<ArticuloDTO>> GetArticuloPorCodigo(string codigo)
        {
            var articulo = await _articuloService.ObtenerPorCodigoAsync(codigo);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }

        [HttpPost]
        public async Task<ActionResult<ArticuloDTO>> PostArticulo(ArticuloCreacionDTO articuloDTO)
        {
            var articulo = await _articuloService.CrearAsync(articuloDTO);
            if (articulo == null)
            {
                return BadRequest("El código del artículo ya existe.");
            }
            return CreatedAtAction(nameof(GetArticulo), new { id = articulo.ArticuloID }, articulo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulo(int id, ArticuloCreacionDTO articuloDTO)
        {
            var resultado = await _articuloService.ActualizarAsync(id, articuloDTO);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("{id}/stock/{cantidad}")]
        public async Task<IActionResult> PatchStock(int id, int cantidad)
        {
            var resultado = await _articuloService.ActualizarStockAsync(id, cantidad);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            var resultado = await _articuloService.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}