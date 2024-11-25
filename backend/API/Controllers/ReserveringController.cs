using AutoMapper;
using BL.Models;
using BL.Models.DTO;
using BL.Models.DTO.Input;
using BL.Models.DTO.Output;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveringController : ControllerBase
    {
        private readonly ReserveringService _service;
        private readonly StorageService _storageService;
        private readonly IMapper _mapper;


        public ReserveringController(ReserveringService reserveringService, StorageService storageService, IMapper mapper)
        {
            _service = reserveringService;
            _storageService = storageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReserveringOutputDTO>>> GetReserveringen()
        {
            try
            {
                var reserveringen = await _service.GetAllReserveringenAsync();
                var reserveringenDtos = _mapper.Map<IEnumerable<ReserveringOutputDTO>>(reserveringen);
                return Ok(reserveringenDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReserveringOutputDTO>> GetReservering(int id)
        {
            try
            {
                var reservering = await _service.GetReserveringByIdAsync(id);
                if (reservering == null)
                {
                    return NotFound();
                }

                var reserveringDTO = _mapper.Map<ReserveringOutputDTO>(reservering);
                return Ok(reserveringDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostReservering([FromBody] ReserveringInputDTO reserveringDTO)
        {
            try
            {
                var reservering = _mapper.Map<Reservering>(reserveringDTO);
                await _service.AddReserveringAsync(reservering);
                return CreatedAtAction(nameof(GetReservering), new { id = reservering.Id }, reservering);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservering(int id, ReserveringInputDTO reserveringDto)
        {
            try
            {
                var reservering = _mapper.Map<Reservering>(reserveringDto);

                if (id != reservering.Id)
                {
                return BadRequest();
            }
            await _service.UpdateReserveringAsync(reservering);
            return NoContent();
        }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservering(int id)
        {
            try
            {
            await _service.DeleteReserveringAsync(id);
            return NoContent();
        }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Check-Out
        [HttpPut("{id}/checkout")]
        public async Task<IActionResult> CheckOut(int id, [FromForm] CheckOutDTO checkOutDTO)
        {
            try
            {
                var reservering = await _service.GetReserveringByIdAsync(id);
                if (reservering == null)
                {
                    return NotFound($"Reservatie niet gevonden");
                }

                reservering.CheckOutState = checkOutDTO.CheckOutState;

                foreach (var file in checkOutDTO.CheckOutPictures)
                {
                    await _storageService.Upload(file);

                    var blobUrl = $"https://fleetmanagercc.blob.core.windows.net/voertuigfotos/{file.FileName}";
                    reservering.CheckOutPictures.Add(blobUrl);
                }

                await _service.UpdateReserveringAsync(reservering);

                return Ok("Check-out is klaar.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Check-In
        [HttpPut("{id}/checkin")]
        public async Task<IActionResult> CheckIn(int id, [FromForm] CheckInDTO checkInDTO)
        {
            try
            {
                var reservering = await _service.GetReserveringByIdAsync(id);
                if (reservering == null)
                {
                    return NotFound($"Reservatie niet gevonden");
                }

                reservering.CheckInState = checkInDTO.CheckInState;
                foreach (var file in checkInDTO.CheckInPictures)
                {
                    await _storageService.Upload(file);
                    var blobUrl = $"https://fleetmanagercc.blob.core.windows.net/voertuigfotos/{file.FileName}{file.FileName}";
                    reservering.CheckInPictures.Add(blobUrl);
                }

                await _service.UpdateReserveringAsync(reservering);

                return Ok("Check-in is klaar.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
