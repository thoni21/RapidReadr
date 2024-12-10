using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidReadr.Server.Models;
using System.Security.Claims;

namespace RapidReadr.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivelyReadingController : ControllerBase
    {

        // get text from db when user wants to read

        // save "timestamp" for what index user is at during reading

        // delete when user is finished 

        // save in db
        [HttpPost]
        public ActionResult<ActivelyReading> HandlePdf(string path)
        {
            // Get the logged-in user's ID from the token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok();
        }
    }
}
