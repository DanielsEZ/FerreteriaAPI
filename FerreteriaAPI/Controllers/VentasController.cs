using FerreteriaAPI.DTOs;
using FerreteriaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FerreteriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentas()
        {
            var ventas = await _ventaService.ObtenerTodasAsync();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> GetVenta(int id)
        {
            var venta = await _ventaService.ObtenerPorIdAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasPorCliente(int clienteId)
        {
            var ventas = await _ventaService.ObtenerPorClienteAsync(clienteId);
            return Ok(ventas);
        }

        [HttpGet("fechas")]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasPorFechas([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var ventas = await _ventaService.ObtenerPorFechasAsync(fechaInicio, fechaFin);
            return Ok(ventas);
        }

        [HttpPost]
        public async Task<ActionResult<VentaDTO>> PostVenta(VentaCreacionDTO ventaDTO)
        {
            var venta = await _ventaService.CrearAsync(ventaDTO);
            if (venta == null)
            {
                return BadRequest("No se pudo crear la venta. Verifique los datos.");
            }
            return CreatedAtAction(nameof(GetVenta), new { id = venta.VentaID }, venta);
        }

        [HttpPatch("{id}/estado/{estado}")]
        public async Task<IActionResult> PatchEstado(int id, string estado)
        {
            var resultado = await _ventaService.ActualizarEstadoAsync(id, estado);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}