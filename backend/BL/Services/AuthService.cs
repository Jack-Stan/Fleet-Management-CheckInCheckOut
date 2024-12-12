using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using BL.Interfaces;
using BL.Models.DTO.Input;
using BL.Models.DTO.Output;
using BL.Models;

namespace BL.Services {
    public class AuthService {
        private readonly IConfiguration _configuration;
        private readonly IGebruikerRepository _gebruikerRepository;

        public AuthService(IConfiguration configuration, IGebruikerRepository gebruikerRepository) {
            _configuration = configuration;
            _gebruikerRepository = gebruikerRepository;
        }

        public async Task<GebruikerOutputDTO?> AuthenticateGebruikerAsync(GebruikerInputDTO gebruikerInput) {
            var gebruiker = await _gebruikerRepository.GetGebruikerByEmailAsync(gebruikerInput.Email);
            if (gebruiker == null || !VerifyPasswordHash(gebruikerInput.Wachtwoord, gebruiker.WachtwoordHash))
                return null;

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, gebruiker.Email),
                new Claim(ClaimTypes.Name, gebruiker.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new GebruikerOutputDTO {
                Id = gebruiker.Id,
                Email = gebruiker.Email,
                Token = tokenString
            };
        }

        public async Task<bool> RegisterGebruikerAsync(GebruikerInputDTO gebruikerInput) {
            var bestaandeGebruiker = await _gebruikerRepository.GetGebruikerByEmailAsync(gebruikerInput.Email);
            if (bestaandeGebruiker != null)
                return false;

            var gebruiker = new Gebruiker {
                Email = gebruikerInput.Email,
                WachtwoordHash = HashPassword(gebruikerInput.Wachtwoord)
            };

            await _gebruikerRepository.AddGebruikerAsync(gebruiker);
            return true;
        }

        private string HashPassword(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPasswordHash(string inputPassword, string storedHash) {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
    }
}
