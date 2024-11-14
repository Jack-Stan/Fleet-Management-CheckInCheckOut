using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;

namespace BL.Interfaces {
    public interface IBestuurderRepository {
        Task<List<Bestuurder>> GetAllBestuurdersAsync();
        Task<Bestuurder> GetBestuurderByIdAsync(int id);
        Task AddBestuurderAsync(Bestuurder bestuurder);
        Task UpdateBestuurderAsync(Bestuurder bestuurder);
        Task DeleteBestuurderAsync(int id);
    }
}
