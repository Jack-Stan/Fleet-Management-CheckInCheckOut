using BL.Interfaces;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveringController : ControllerBase {
        private readonly ReserveringService _service;

        public ReserveringController(ReserveringService reserveringService) {
            _service = reserveringService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservering>>> GetReserveringen() {
            try {
                var reserveringen = await _service.GetAllReserveringenAsync();
                return Ok(reserveringen);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservering>> GetReservering(int id) {
            try {
                var reservering = await _service.GetReserveringByIdAsync(id);
                if (reservering == null) {
                    return NotFound();
                }
                return Ok(reservering);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostReservering([FromBody] Reservering reservering) {
            try {
                await _service.AddReserveringAsync(reservering);
                return CreatedAtAction(nameof(GetReservering), new { id = reservering.Id }, reservering);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservering(int id, Reservering reservering) {
            if (id != reservering.Id) {
                return BadRequest();
            }
            await _service.UpdateReserveringAsync(reservering);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservering(int id) {
            await _service.DeleteReserveringAsync(id);
            return NoContent();
        }
    }
}
