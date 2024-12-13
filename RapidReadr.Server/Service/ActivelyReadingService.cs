using RapidReadr.Server.Models;
using RapidReadr.Server.Repository.Interfaces;

namespace RapidReadr.Server.Service
{
    public class ActivelyReadingService
    {
        private readonly IActivelyReadingRepository _activelyReadingRepository;

        public ActivelyReadingService(IActivelyReadingRepository activelyReadingRepository) {
            _activelyReadingRepository = activelyReadingRepository;
        }

        public async Task<IEnumerable<ActivelyReading>> GetAllAsync()
        {
            return await _activelyReadingRepository.GetAllAsync();
        }

        public async Task<ActivelyReading> GetByIdAsync(int id)
        {
            return await _activelyReadingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ActivelyReading>> GetAllByUserIdAsync(string id)
        {
            return await _activelyReadingRepository.GetAllByUserIdAsync(id);
        }

        public async Task AddAsync(ActivelyReading entity)
        {
            await _activelyReadingRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ActivelyReading entity)
        {
            await _activelyReadingRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _activelyReadingRepository.DeleteAsync(id);
        }
    }
}
