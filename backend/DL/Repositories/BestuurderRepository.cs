using BL.Interfaces;
using BL.Models;
using DL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DL.Repositories {
    public class BestuurderRepository : IBestuurderRepository {
        private readonly FleetManagementDbContext _context;

        public BestuurderRepository(FleetManagementDbContext context) {
            _context = context;
        }

        public async Task AddBestuurderAsync(Bestuurder bestuurder) {
            try {
                await _context.Bestuurders.AddAsync(bestuurder);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to add driver: {ex.Message}", ex);
            }
        }

        public async Task DeleteBestuurderAsync(int id) {
            try {
                var bestuurder = await _context.Bestuurders.FindAsync(id);
                if (bestuurder != null) {
                    _context.Bestuurders.Remove(bestuurder);
                    await _context.SaveChangesAsync();
                }
            } catch (Exception ex) {
                throw new Exception($"Failed to delete driver with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<Bestuurder>> GetAllBestuurdersAsync() {
            try {
                return await _context.Bestuurders.ToListAsync();
            } catch (Exception ex) {
                throw new Exception("Failed to retrieve drivers: " + ex.Message, ex);
            }
        }

        public async Task<Bestuurder> GetBestuurderByIdAsync(int id) {
            try {
                return await _context.Bestuurders.FindAsync(id);
            } catch (Exception ex) {
                throw new Exception($"Failed to retrieve driver with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task UpdateBestuurderAsync(Bestuurder bestuurder) {
            try {
                _context.Bestuurders.Update(bestuurder);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to update driver with ID {bestuurder.Id}: {ex.Message}", ex);
            }
        }
    }
}
