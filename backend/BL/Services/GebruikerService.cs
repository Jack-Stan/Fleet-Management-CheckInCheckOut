using BL.Interfaces;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services {
    public class GebruikerService {
        private readonly IGebruikerRepository _repo;

        public GebruikerService(IGebruikerRepository repo) {
            _repo = repo;
        }

        public async Task<IEnumerable<Gebruiker>> GetAllGebruikersAsync() {
            return await _repo.GetAllGebruikersAsync();
        }

        public async Task<Gebruiker> GetGebruikerByIdAsync(int id) {
            var gebruiker = await _repo.GetGebruikerByIdAsync(id);
            if (gebruiker == null) throw new Exception($"Gebruiker met ID {id} niet gevonden.");
            return gebruiker;
        }

        public async Task AddGebruikerAsync(Gebruiker gebruiker) {
            gebruiker.WachtwoordHash = HashWachtwoord(gebruiker.WachtwoordHash);
            await _repo.AddGebruikerAsync(gebruiker);
        }

        public async Task DeleteGebruikerAsync(int id) {
            var gebruiker = await _repo.GetGebruikerByIdAsync(id);
            if (gebruiker == null) throw new Exception($"Gebruiker met ID {id} niet gevonden.");
            await _repo.DeleteGebruikerAsync(id);
        }

        public async Task ChangePasswordAsync(int id, string newPassword) {
            var gebruiker = await _repo.GetGebruikerByIdAsync(id);
            if (gebruiker == null) throw new Exception($"Gebruiker met ID {id} niet gevonden.");

            var newPasswordHash = HashWachtwoord(newPassword);
            await _repo.ChangePasswordAsync(id, newPasswordHash);
        }

        private string HashWachtwoord(string wachtwoord) {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(wachtwoord);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
