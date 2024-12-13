using Microsoft.EntityFrameworkCore;
using RapidReadr.Server.Data;
using RapidReadr.Server.Models;
using RapidReadr.Server.Repository.Interfaces;

namespace RapidReadr.Server.Repository
{
    public class ActivelyReadingRepository : IActivelyReadingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActivelyReading> _dbSet;

        public ActivelyReadingRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ActivelyReading>();
        }

        public async Task<IEnumerable<ActivelyReading>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<ActivelyReading> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<ActivelyReading>> GetAllByUserIdAsync(string id)
        {
            return await _dbSet.Where(x => x.userId == id).ToListAsync();
        }

        public async Task AddAsync(ActivelyReading entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ActivelyReading entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
