using AutoMapper;
using BL.Models;
using BL.Models.DTO.Input;
using BL.Models.DTO.Output;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VoertuigController : ControllerBase {
        private readonly VoertuigService _service;
        private readonly IMapper _mapper;

        public VoertuigController(VoertuigService voertuigService, IMapper mapper) {
            _service = voertuigService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoertuigOutputDTO>>> GetVoertuigen() {
            try {
                var voertuigen = await _service.GetAllVoertuigenAsync();
                var voertuigenDtos = _mapper.Map<IEnumerable<VoertuigOutputDTO>>(voertuigen);
                return Ok(voertuigenDtos);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoertuigOutputDTO>> GetVoertuig(int id) {
            try {
                var voertuig = await _service.GetVoertuigAsync(id);
                if (voertuig == null) {
                    return NotFound();
                }
                var voertuigDto = _mapper.Map<VoertuigOutputDTO>(voertuig);
                return Ok(voertuigDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostVoertuig([FromBody] VoertuigInputDTO voertuigDto) {
            try {
                var voertuig = _mapper.Map<Voertuig>(voertuigDto);
                await _service.AddVoertuigAsync(voertuig);
                var createdVoertuigDto = _mapper.Map<VoertuigOutputDTO>(voertuig);
                return CreatedAtAction(nameof(GetVoertuig), new { id = voertuig.Id }, createdVoertuigDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoertuig(int id, [FromBody] VoertuigInputDTO voertuigDto) {
            try {
                var voertuig = _mapper.Map<Voertuig>(voertuigDto);
                voertuig.Id = id; 

                if (id != voertuig.Id) {
                    return BadRequest();
                }

                await _service.UpdateVoertuigen(voertuig);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoertuig(int id) {
            try {
                await _service.DeleteVoertuigen(id);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
