using BL.Interfaces;
using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services {
    public class ReserveringService {
        private readonly IReserveringRepository repo;

        public ReserveringService(IReserveringRepository _repo) {
            repo = _repo;
        }

        public async Task<IEnumerable<Reservering>> GetAllReserveringenAsync() {
            return await repo.GetAllReserveringenAsync();
        }

        public async Task<Reservering> GetReserveringByIdAsync(int id) {
            return await repo.GetReserveringByIdAsync(id);
        }

        public async Task AddReserveringAsync(Reservering reservering) {
            await repo.AddReserveringAsync(reservering);
        }

        public async Task UpdateReserveringAsync(Reservering reservering) {
            await repo.UpdateReserveringAsync(reservering);
        }

        public async Task DeleteReserveringAsync(int id) {
            await repo.DeleteReserveringAsync(id);
        }
    }
}
