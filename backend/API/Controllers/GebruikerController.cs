using AutoMapper;
using BL.Models;
using BL.Models.DTO.Input;
using BL.Models.DTO.Output;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase {
        private readonly GebruikerService _service;
        private readonly IMapper _mapper;

        public GebruikerController(GebruikerService gebruikerService, IMapper mapper) {
            _service = gebruikerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GebruikerOutputDTO>>> GetAllGebruikers() {
            try {
                var gebruikers = await _service.GetAllGebruikersAsync();
                var gebruikersDto = _mapper.Map<IEnumerable<GebruikerOutputDTO>>(gebruikers);
                return Ok(gebruikersDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GebruikerOutputDTO>> GetGebruikerById(int id) {
            try {
                var gebruiker = await _service.GetGebruikerByIdAsync(id);
                if (gebruiker == null) {
                    return NotFound();
                }

                var gebruikerDto = _mapper.Map<GebruikerOutputDTO>(gebruiker);
                return Ok(gebruikerDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddGebruiker([FromBody] GebruikerInputDTO gebruikerDto) {
            try {
                var gebruiker = _mapper.Map<Gebruiker>(gebruikerDto);
                await _service.AddGebruikerAsync(gebruiker);
                var outputDto = _mapper.Map<GebruikerOutputDTO>(gebruiker);
                return CreatedAtAction(nameof(GetGebruikerById), new { id = gebruiker.Id }, outputDto);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGebruiker(int id) {
            try {
                await _service.DeleteGebruikerAsync(id);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] string newPassword) {
            try {
                await _service.ChangePasswordAsync(id, newPassword);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
