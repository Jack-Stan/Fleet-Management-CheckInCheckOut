using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;

namespace BL.Interfaces {
    public interface IReserveringRepository {
        Task<List<Reservering>> GetAllReserveringenAsync();
        Task<Reservering> GetReserveringByIdAsync(int id);
        Task AddReserveringAsync(Reservering reservering);
        Task UpdateReserveringAsync(Reservering reservering);
        Task DeleteReserveringAsync(int id);
    }
}
