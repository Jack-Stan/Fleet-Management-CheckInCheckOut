using BL.Interfaces;
using BL.Models;
using DL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repositories
{
    public class VoertuigRepository : IVoertuigRepository
    {
        private readonly FleetManagementDbContext _context;
        public VoertuigRepository(FleetManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddVoertuigAsync(Voertuig voertuig)
        {
            try
            {
                await _context.Voertuigen.AddAsync(voertuig);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteVoertuigAsync(int id)
        {
            var voertuig = await _context.Voertuigen.FindAsync(id);
            if (voertuig != null)
            {
                _context.Voertuigen.Remove(voertuig);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Voertuig>> GetAllVoertuigenAsync()
        {
            return await _context.Voertuigen.ToListAsync();
        }

        public async Task<Voertuig> GetVoertuigByIdAsync(int id)
        {
            var voertuig = await _context.Voertuigen.FindAsync(id);
            if ( voertuig == null )
            {
                return null;
            }
            return voertuig;
        }

        public async Task UpdateVoertuigAsync(Voertuig voertuig)
        {
            _context.Voertuigen.Update(voertuig);
            await _context.SaveChangesAsync();
        }
    }
}
