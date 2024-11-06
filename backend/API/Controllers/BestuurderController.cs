using BL.Interfaces;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class BestuurderController : ControllerBase {
        private readonly BestuurderService _service;

        public BestuurderController(BestuurderService bestuurderService) {
            _service = bestuurderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bestuurder>>> GetBestuurders() {
            try {
                var bestuurders = await _service.GetAllBestuurdersAsync();
                return Ok(bestuurders);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bestuurder>> GetBestuurder(int id) {
            try {
                var bestuurder = await _service.GetBestuurderAsync(id);
                if (bestuurder == null) {
                    return NotFound();
                }
                return Ok(bestuurder);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostBestuurder([FromBody] Bestuurder bestuurder) {
            try {
                await _service.AddBestuurderAsync(bestuurder);
                return CreatedAtAction(nameof(GetBestuurder), new { id = bestuurder.Id }, bestuurder);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestuurder(int id, Bestuurder bestuurder) {
            if (id != bestuurder.Id) {
                return BadRequest();
            }
            await _service.UpdateBestuurderAsync(bestuurder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestuurder(int id) {
            await _service.DeleteBestuurderAsync(id);
            return NoContent();
        }
    }
}
