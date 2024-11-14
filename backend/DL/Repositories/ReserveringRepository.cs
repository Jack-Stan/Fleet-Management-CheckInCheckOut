using BL.Interfaces;
using BL.Models;
using DL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DL.Repositories {
    public class ReserveringRepository : IReserveringRepository {
        private readonly FleetManagementDbContext _context;

        public ReserveringRepository(FleetManagementDbContext context) {
            _context = context;
        }

        public async Task AddReserveringAsync(Reservering reservering) {
            try {
                await _context.Reserveringen.AddAsync(reservering);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to add reservation: {ex.Message}", ex);
            }
        }

        public async Task DeleteReserveringAsync(int id) {
            try {
                var reservering = await _context.Reserveringen.FindAsync(id);
                if (reservering != null) {
                    _context.Reserveringen.Remove(reservering);
                    await _context.SaveChangesAsync();
                }
            } catch (Exception ex) {
                throw new Exception($"Failed to delete reservation with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<Reservering>> GetAllReserveringenAsync() {
            try {
                return await _context.Reserveringen.ToListAsync();
            } catch (Exception ex) {
                throw new Exception("Failed to retrieve reservations: " + ex.Message, ex);
            }
        }

        public async Task<Reservering> GetReserveringByIdAsync(int id) {
            try {
                return await _context.Reserveringen.FindAsync(id);
            } catch (Exception ex) {
                throw new Exception($"Failed to retrieve reservation with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task UpdateReserveringAsync(Reservering reservering) {
            try {
                _context.Reserveringen.Update(reservering);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception($"Failed to update reservation with ID {reservering.Id}: {ex.Message}", ex);
            }
        }
    }
}
