using Microsoft.AspNetCore.Mvc;
using RapidReadr.Server.Models;
using RapidReadr.Server.Service;

namespace RapidReadr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivelyReadingController : ControllerBase
    {
        private readonly ActivelyReadingService _activelyReadingService;

        public ActivelyReadingController(ActivelyReadingService activelyReadingService) {
            _activelyReadingService = activelyReadingService;
        }

        [HttpGet]
        public async Task<IEnumerable<ActivelyReading>> GetAll()
        {
            return await _activelyReadingService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActivelyReading> GetById(int id) { 
            return await _activelyReadingService.GetByIdAsync(id);
        }

        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<ActivelyReading>> GetAllByUserId(string userId)
        {
            return await _activelyReadingService.GetAllByUserIdAsync(userId);
        }

        [HttpPost]
        public async Task Add(ActivelyReading activelyReading)
        {
            await _activelyReadingService.AddAsync(activelyReading);
        }

        [HttpPut]
        public async Task Update(ActivelyReading activelyReading) {
            await _activelyReadingService.UpdateAsync(activelyReading);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _activelyReadingService.DeleteAsync(id);
        }
    }
}
