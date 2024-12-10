using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidReadr.Server.Data;
using RapidReadr.Server.Models;
using System.Security.Claims;

namespace RapidReadr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivelyReadingController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ActivelyReadingController(ApplicationDbContext dbContext) { 
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ActivelyReading>> HandlePdf()
        {
            return _dbContext.ActivelyReadings.ToList();
        }

        // get text from db when user wants to read

        // save "timestamp" for what index user is at during reading

        // delete when user is finished 

        // save in db       
    }
}
