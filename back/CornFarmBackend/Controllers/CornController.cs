using CornFarmBackend.Data;
using CornFarmBackend.Models;
using Microsoft.AspNetCore.Mvc;


namespace CornFarmBackend.Controllers
{
    public class CornResponse
    {
        public string message { get; set; }
        public int cornCount { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CornController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CornController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("buy")]
        public IActionResult BuyCorn([FromBody] CornRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("Username is required.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
            {
                user = new User { Username = request.Username };
                _context.Users.Add(user);
            }

            var now = DateTime.UtcNow;

            if (user.LastTimeCornBought.HasValue &&
                (now - user.LastTimeCornBought.Value).TotalMinutes < 1)
            {
                return StatusCode(429, "Too many requests. Wait a minute before buying more corn.");
            }

            user.LastTimeCornBought = now;
            user.CornBought += 1;

            _context.SaveChanges();
            return Ok(new CornResponse{ message = "200 🌽", cornCount = user.CornBought });
        }
    }

    public class CornRequest
    {
        public string Username { get; set; }
    }
}
