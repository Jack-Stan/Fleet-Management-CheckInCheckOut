using BL.Interfaces;
using BL.Models;
using DL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DL.Repositories {
    public class GebruikerRepository : IGebruikerRepository {
        private readonly FleetManagementDbContext _context;

        public GebruikerRepository(FleetManagementDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Gebruiker>> GetAllGebruikersAsync() {
            try {
                return await _context.Gebruikers.ToListAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to retrieve all users: {ex.Message}", ex);
            }
        }

        public async Task<Gebruiker> GetGebruikerByIdAsync(int id) {
            try {
                return await _context.Gebruikers.FindAsync(id);
            } catch (Exception ex) {
                throw new Exception($"Failed to retrieve user with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Gebruiker> GetGebruikerByEmailAsync(string email) {
            try {
                return await _context.Gebruikers.FirstOrDefaultAsync(g => g.Email == email);
            } catch (Exception ex) {
                throw new Exception($"Failed to retrieve user with email {email}: {ex.Message}", ex);
            }
        }

        public async Task AddGebruikerAsync(Gebruiker gebruiker) {
            try {
                await _context.Gebruikers.AddAsync(gebruiker);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to add user: {ex.Message}", ex);
            }
        }

        public async Task DeleteGebruikerAsync(int id) {
            try {
                var gebruiker = await _context.Gebruikers.FindAsync(id);
                if (gebruiker == null) throw new Exception($"Gebruiker met ID {id} niet gevonden.");

                _context.Gebruikers.Remove(gebruiker);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to delete user with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task ChangePasswordAsync(int id, string newPasswordHash) {
            try {
                var gebruiker = await _context.Gebruikers.FindAsync(id);
                if (gebruiker == null) throw new Exception($"Gebruiker met ID {id} niet gevonden.");

                gebruiker.WachtwoordHash = newPasswordHash;
                _context.Gebruikers.Update(gebruiker);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to change password for user with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
