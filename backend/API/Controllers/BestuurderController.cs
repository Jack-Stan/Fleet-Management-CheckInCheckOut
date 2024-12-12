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
    public class BestuurderController : ControllerBase {
        private readonly BestuurderService _service;
        private readonly IMapper _mapper;

        public BestuurderController(BestuurderService bestuurderService, IMapper mapper) {
            _service = bestuurderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BestuurderOutputDTO>>> GetBestuurders() {
            try {
                var bestuurders = await _service.GetAllBestuurdersAsync();
                var bestuurdersDto = _mapper.Map<IEnumerable<BestuurderOutputDTO>>(bestuurders);
                return Ok(bestuurdersDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BestuurderOutputDTO>> GetBestuurder(int id) {
            try {
                var bestuurder = await _service.GetBestuurderAsync(id);
                if (bestuurder == null) {
                    return NotFound();
                }

                var bestuurderDto = _mapper.Map<BestuurderOutputDTO>(bestuurder);
                return Ok(bestuurderDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostBestuurder([FromBody] BestuurderInputDTO bestuurderDto) {
            try {
                var bestuurder = _mapper.Map<Bestuurder>(bestuurderDto);
                await _service.AddBestuurderAsync(bestuurder);
                var outputDto = _mapper.Map<BestuurderOutputDTO>(bestuurder);
                return CreatedAtAction(nameof(GetBestuurder), new { id = bestuurder.Id }, outputDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestuurder(int id, [FromBody] BestuurderInputDTO bestuurderDto) {
            try {
                var bestuurder = _mapper.Map<Bestuurder>(bestuurderDto);
                if (id != bestuurder.Id) {
                    return BadRequest("ID komt niet overeen.");
                }

                await _service.UpdateBestuurderAsync(bestuurder);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestuurder(int id) {
            try {
                await _service.DeleteBestuurderAsync(id);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
