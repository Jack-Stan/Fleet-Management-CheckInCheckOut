using BL.Interfaces;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoertuigController : ControllerBase
    {
        private readonly VoertuigService service;
        public VoertuigController(VoertuigService voertuigService)
        {
            service = voertuigService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voertuig>>> GetVoertuigen()
        {
            try
            {
                var voertuigen = await service.GetAllVoertuigenAsync();
                return Ok(voertuigen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Voertuig>> GetVoertuig(int id)
        {
            try
            {
                var voertuig = await service.GetVoertuigAsync(id);
                if (voertuig == null)
                {
                    return NotFound();
                }
                return Ok(voertuig);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostVoertuig([FromBody] Voertuig voertuig)
        {
            try
            {
                await service.AddVoertuigAsync(voertuig);
                return CreatedAtAction(nameof(GetVoertuig), new { id = voertuig.Id }, voertuig);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoertuig(int id, Voertuig voertuig)
        {
            if (id != voertuig.Id)
            {
                return BadRequest();
            }
            await service.UpdateVoertuigen(voertuig);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoertuig(int id)
        {
            await service.DeleteVoertuigen(id);
            return NoContent();
        }
    }
}
