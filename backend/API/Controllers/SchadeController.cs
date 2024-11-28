using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchadeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public SchadeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload-checkin-schade")]
        public async Task<IActionResult> UploadCheckInSchade([FromForm] IFormFile schadeFoto, [FromForm] string reserveringsnummer)
        {
            if (schadeFoto == null || schadeFoto.Length == 0)
            {
                return BadRequest("Er is geen bestand geselecteerd.");
            }

            if (string.IsNullOrEmpty(reserveringsnummer))
            {
                return BadRequest("Reserveringsnummer is vereist.");
            }

            var bestandsnaam = $"{reserveringsnummer}_{DateTime.Now:yyyyMMddHHmmss}_{schadeFoto.FileName}";
            var filePath = Path.Combine(_uploadPath, bestandsnaam);

            // Foto opslaan
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await schadeFoto.CopyToAsync(fileStream);
            }

            // Schade-informatie opslaan
            var schade = new CheckInSchade
            {
                Reserveringsnummer = reserveringsnummer,
                FotoPad = filePath,
                SchadeType = "Kras",  // Standaard, kan door de gebruiker aangepast worden
                Datum = DateTime.Now
            };

            _context.CheckInSchades.Add(schade);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Schadefoto succesvol geüpload", filePath = filePath });
        }

        [HttpPost("upload-checkout-schade")]
        public async Task<IActionResult> UploadCheckOutSchade([FromForm] IFormFile schadeFoto, [FromForm] string reserveringsnummer)
        {
            if (schadeFoto == null || schadeFoto.Length == 0)
            {
                return BadRequest("Er is geen bestand geselecteerd.");
            }

            if (string.IsNullOrEmpty(reserveringsnummer))
            {
                return BadRequest("Reserveringsnummer is vereist.");
            }

            var bestandsnaam = $"{reserveringsnummer}_{DateTime.Now:yyyyMMddHHmmss}_{schadeFoto.FileName}";
            var filePath = Path.Combine(_uploadPath, bestandsnaam);

            // Foto opslaan
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await schadeFoto.CopyToAsync(fileStream);
            }

            // Schade-informatie opslaan
            var schade = new CheckOutSchade
            {
                Reserveringsnummer = reserveringsnummer,
                FotoPad = filePath,
                SchadeType = "Deuk",  // Standaard, kan door de gebruiker aangepast worden
                Datum = DateTime.Now
            };

            _context.CheckOutSchades.Add(schade);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Schadefoto succesvol geüpload", filePath = filePath });
        }
    }
}
