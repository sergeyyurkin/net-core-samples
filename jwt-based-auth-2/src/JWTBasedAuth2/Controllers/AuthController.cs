using System.Linq;
using System.Threading.Tasks;
using JWTBasedAuth2.Data;
using JWTBasedAuth2.Model;
using JWTBasedAuth2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTBasedAuth2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;

        public AuthController(AppDbContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (dbUser != null)
            {
                // Пароль для упрощения храниться в чистом виде!
                // В реальных проектах, необходимо хранить пароль в виде хэша с солью.

                if (dbUser.Password == user.Password)
                {
                    var token = _tokenBuilder.BuildToken(user.Username);
                    return Ok(token);
                }

                return BadRequest("Could not authenticate user.");
            }

            return NotFound("User not found.");
        }

        [HttpGet("verify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VerifyToken()
        {
            var claim = User.Claims.SingleOrDefault();
            if (claim != null)
            {
                if (await _context.Users.AnyAsync(u => u.Username == claim.Value))
                {
                    return NoContent();
                }
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (dbUser == null)
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                var token = _tokenBuilder.BuildToken(user.Username);

                return Ok(token);
            }

            return BadRequest("User already exist.");
        }
    }
}
