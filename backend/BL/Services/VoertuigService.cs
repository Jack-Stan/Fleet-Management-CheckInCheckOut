using BL.Interfaces;
using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class VoertuigService
    {
        private readonly IVoertuigRepository repo;

        public VoertuigService(IVoertuigRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<Voertuig>> GetAllVoertuigenAsync()
        {
            return await repo.GetAllVoertuigenAsync();
        }

        public async Task<Voertuig> GetVoertuigAsync(int id)
        {
            return await repo.GetVoertuigByIdAsync(id);
        }

        public async Task AddVoertuigAsync(Voertuig voertuig)
        {
            await repo.AddVoertuigAsync(voertuig);
        }

        public async Task UpdateVoertuigen(Voertuig voertuig)
        {
            await repo.UpdateVoertuigAsync(voertuig);
        }

        public async Task DeleteVoertuigen(int id)
        {
            await repo.DeleteVoertuigAsync(id);
        }
    }
}
