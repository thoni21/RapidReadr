using RapidReadr.Server.Models;

namespace RapidReadr.Server.Repository.Interfaces
{
    public interface IActivelyReadingRepository 
    {
        Task<IEnumerable<ActivelyReading>> GetAllAsync();
        Task<ActivelyReading> GetByIdAsync(int id);
        Task<IEnumerable<ActivelyReading>> GetAllByUserIdAsync(string id);
        Task AddAsync(ActivelyReading entity);
        Task UpdateAsync(ActivelyReading entity);
        Task DeleteAsync(int id);
    }
}
