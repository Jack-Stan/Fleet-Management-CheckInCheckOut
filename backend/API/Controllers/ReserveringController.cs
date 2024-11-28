using Microsoft.AspNetCore.Mvc;
using BL.Models;
using DL.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveringController : ControllerBase
    {
        private readonly FleetManagementDbContext _context;

        public ReserveringController(FleetManagementDbContext context)
        {
            _context = context;
        }

        [HttpPost("{reserveringId}/upload-checkin-foto")]
        public async Task<IActionResult> UploadCheckInFoto(int reserveringId, [FromForm] IFormFile foto)
        {
            var reservering = await _context.Reserveringen.FindAsync(reserveringId);
            if (reservering == null)
            {
                return NotFound("Reservering niet gevonden");
            }

            // Controleer of er een bestand is geüpload
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("Geen foto geselecteerd");
            }

            // Opslaan van de foto (bijv. in de lokale opslag of een blob storage)
            var filePath = Path.Combine("uploads", foto.FileName); // Bijv. de foto lokaal opslaan
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Foto toevoegen aan CheckInPictures van de reservering
            reservering.CheckInPictures.Add(filePath);
            _context.Reserveringen.Update(reservering);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Foto geüpload", fotoPad = filePath });
        }

        [HttpPost("{reserveringId}/upload-checkout-foto")]
        public async Task<IActionResult> UploadCheckOutFoto(int reserveringId, [FromForm] IFormFile foto)
        {
            var reservering = await _context.Reserveringen.FindAsync(reserveringId);
            if (reservering == null)
            {
                return NotFound("Reservering niet gevonden");
            }

            if (foto == null || foto.Length == 0)
            {
                return BadRequest("Geen foto geselecteerd");
            }

            // Opslaan van de foto (bijv. in de lokale opslag of een blob storage)
            var filePath = Path.Combine("uploads", foto.FileName); // Bijv. de foto lokaal opslaan
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Foto toevoegen aan CheckOutPictures van de reservering
            reservering.CheckOutPictures.Add(filePath);
            _context.Reserveringen.Update(reservering);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Foto geüpload", fotoPad = filePath });
        }
    }
}
