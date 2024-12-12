using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces {
    public interface IGebruikerRepository {
        Task<IEnumerable<Gebruiker>> GetAllGebruikersAsync();
        Task<Gebruiker> GetGebruikerByIdAsync(int id);
        Task<Gebruiker> GetGebruikerByEmailAsync(string email);
        Task AddGebruikerAsync(Gebruiker gebruiker);
        Task DeleteGebruikerAsync(int id);
        Task ChangePasswordAsync(int id, string newPasswordHash);
    }
}
