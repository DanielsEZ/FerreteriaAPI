using FerreteriaAPI.DTOs;
using FerreteriaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class FormasPagoController : ControllerBase
    {
        private readonly IFormaPagoService _formaPagoService;

        public FormasPagoController(IFormaPagoService formaPagoService)
        {
            _formaPagoService = formaPagoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPagoDTO>>> GetFormasPago()
        {
            var formasPago = await _formaPagoService.ObtenerTodasAsync();
            return Ok(formasPago);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPagoDTO>> GetFormaPago(int id)
        {
            var formaPago = await _formaPagoService.ObtenerPorIdAsync(id);
            if (formaPago == null)
            {
                return NotFound();
            }
            return Ok(formaPago);
        }

        [HttpPost]
        public async Task<ActionResult<FormaPagoDTO>> PostFormaPago(FormaPagoCreacionDTO formaPagoDTO)
        {
            var formaPago = await _formaPagoService.CrearAsync(formaPagoDTO);
            return CreatedAtAction(nameof(GetFormaPago), new { id = formaPago!.FormaPagoID }, formaPago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPago(int id, FormaPagoCreacionDTO formaPagoDTO)
        {
            var resultado = await _formaPagoService.ActualizarAsync(id, formaPagoDTO);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPago(int id)
        {
            var resultado = await _formaPagoService.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}