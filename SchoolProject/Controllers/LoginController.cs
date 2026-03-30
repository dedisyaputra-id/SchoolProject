using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly ILogger<LoginController> _logger;
        public LoginController(AppDbContext context, IPasswordHasher hasher, ILogger<LoginController> logger)
        {
            _context = context;
            _hasher = hasher;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO req)
        {
            _logger.LogInformation("Login attempt for email: {Email}", req.Email);

            if (string.IsNullOrEmpty(req.Email) || string.IsNullOrWhiteSpace(req.Email))
            {
                return BadRequest(new { message = "Email is required" });
            }
            if (string.IsNullOrEmpty(req.Password) || string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest(new { message = "Password is required" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == req.Email);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            var isPasswordValid = _hasher.VerifyPassword(user.PasswordHash, req.Password);

            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "User name or password is not valid" });
            }

            var token = generateTokenJwt(user);

            return Ok(new { message = "Login berhasil", data = user, token = token });
        }

        private string generateTokenJwt(User User)
        {
            try
            {
                _logger.LogInformation("Generating JWT token for user: {UserId}", User.Id);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secrete_banget_nih_key_893489hf949h_ihiweruh9834hgb4398_osdhg489h34"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim(ClaimTypes.Name, User.Name),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Role, User.Role),
                new Claim("tenantId",  User.TenantId.ToString() ?? "")
            };

                var token = new JwtSecurityToken(
                    issuer: "school_project_system",
                    audience: "user_school_project",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while generating JWT token for user: {UserId}", User.Id);
                throw;
            }
        }
    }
}
