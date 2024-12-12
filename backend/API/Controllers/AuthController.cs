using BL.Models.DTO.Input;
using BL.Models.DTO.Output;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] GebruikerInputDTO registratieDto) {
            var resultaat = await _authService.RegisterGebruikerAsync(registratieDto);

            if (!resultaat)
                return BadRequest(new { Message = "Registration failed. User might already exist." });

            return Ok(new { Message = "User successfully registered" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] GebruikerInputDTO loginDto) {
            var gebruikerOutput = await _authService.AuthenticateGebruikerAsync(loginDto);

            if (gebruikerOutput == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            return Ok(gebruikerOutput);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout() {
            return Ok(new { Message = "Successfully logged out" });
        }
    }
}
