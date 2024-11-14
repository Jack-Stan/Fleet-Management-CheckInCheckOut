using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IVoertuigRepository
    {
        Task<List<Voertuig>> GetAllVoertuigenAsync();
        Task<Voertuig> GetVoertuigByIdAsync(int id);
        Task AddVoertuigAsync(Voertuig voertuig);
        Task UpdateVoertuigAsync(Voertuig voertuig);
        Task DeleteVoertuigAsync(int id);
    }
}
