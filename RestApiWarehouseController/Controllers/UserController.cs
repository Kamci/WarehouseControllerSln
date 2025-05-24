using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestApiWarehouseController.Models;
using RestApiWarehouseController.Models.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestApiWarehouseController.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WCContext _context;
        private readonly IConfiguration _config;

        public UserController(WCContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
         .OrderByDescending(u => u.Id)
         .ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                using var sha = SHA256.Create();
                user.PasswordHash = sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(user).Property(u => u.PasswordHash).IsModified = !string.IsNullOrWhiteSpace(user.Password);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                using var sha = SHA256.Create();
                user.PasswordHash = sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            }
            else
            {
                return BadRequest("Password is required.");
            }
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
            if (existing != null)
                return Conflict("Login already exists.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized();

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Login),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var keyString = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("JWT Key is missing in configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                login = user.Login,
                role = user.Role,
                 userId = user.Id
            });
        }
        private bool VerifyPassword(string password, byte[] passwordHash)
        {
            using var sha = SHA256.Create();
            var inputHash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return inputHash.SequenceEqual(passwordHash);
        }
    }
}
