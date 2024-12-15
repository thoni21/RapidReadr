using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidReadr.Server.Models;
using RapidReadr.Server.Service;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("user")]
        public async Task<IEnumerable<ActivelyReading>> GetAllByUserId()
        {
            if (User.FindFirstValue(ClaimTypes.Email) is not string _userId)
            {
                throw new InvalidOperationException("No user found.");
            }

            return await _activelyReadingService.GetAllByUserIdAsync(_userId);
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
