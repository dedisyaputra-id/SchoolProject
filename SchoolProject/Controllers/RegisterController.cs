using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUserRepository _userRepository;
        private readonly ITenantRepository _tenantRepo;
        public RegisterController(IPasswordHasher hasher, IUserRepository userRepo, ITenantRepository tenantRepo)
        {
            _hasher = hasher;
            _userRepository = userRepo;
            _tenantRepo = tenantRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            try
            {
                // ✅ Validasi
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("Email and password are required.");
                }

                // ✅ Cek user
                var existingUser = await _userRepository.GetByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return Conflict("User already exists.");
                }

                // ✅ Hash password
                var hashedPassword = _hasher.HashPassword(request.Password);

                // ✅ Buat tenant
                var tenant = new Tenant("Sekolah A");
                _tenantRepo.AddTenantAsync(tenant); 

                // ✅ Buat user (pakai tenant.Id yang valid)
                var user = new User(
                    request.Name,
                    request.Email,
                    request.Role,
                    hashedPassword,
                    tenant.Id// 🔥 JANGAN ToString()
                );

                _userRepository.AddAsync(user); 

                // ✅ Return token
                return Ok(new {data = user, message = "success create data user"});
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
