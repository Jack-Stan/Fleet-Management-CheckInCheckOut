using BL.Interfaces;
using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services {
    public class BestuurderService {
        private readonly IBestuurderRepository repo;

        public BestuurderService(IBestuurderRepository _repo) {
            repo = _repo;
        }

        public async Task<IEnumerable<Bestuurder>> GetAllBestuurdersAsync() {
            return await repo.GetAllBestuurdersAsync();
        }

        public async Task<Bestuurder> GetBestuurderAsync(int id) {
            return await repo.GetBestuurderByIdAsync(id);
        }

        public async Task AddBestuurderAsync(Bestuurder bestuurder) {
            await repo.AddBestuurderAsync(bestuurder);
        }

        public async Task UpdateBestuurderAsync(Bestuurder bestuurder) {
            await repo.UpdateBestuurderAsync(bestuurder);
        }

        public async Task DeleteBestuurderAsync(int id) {
            await repo.DeleteBestuurderAsync(id);
        }
    }
}
