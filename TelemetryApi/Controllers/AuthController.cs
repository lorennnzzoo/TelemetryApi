using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Telemetry.Data.Dtos;
using Telemetry.Business;

namespace TelemetryApi.Controllers
{
    [Authorize(Roles = "PcbAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Telemetry.Data.Models.TelemetryapiContext context;
        public AuthController(Telemetry.Data.Models.TelemetryapiContext _context)
        {
            context = _context;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto login)
        {
            var user = context.Users.Where(e => e.Username == login.Username).FirstOrDefault();

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            if (user.FailedAttempts >= 3)
                return Unauthorized("Account is locked due to multiple failed attempts");

            bool isPasswordValid = Hashing.Verify(login.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                user.FailedAttempts += 1;
                context.SaveChanges();
                return Unauthorized("Invalid credentials");
            }

            user.FailedAttempts = 0;
            user.LastLogin = DateTime.Now;
            context.SaveChanges();

            //string token = GenerateJwtToken(user);
            return Ok();
        }
    }
}
