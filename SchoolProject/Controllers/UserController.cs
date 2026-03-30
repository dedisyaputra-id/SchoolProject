using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SchoolProject.Controllers
{
    [Authorize(Roles = "superadmin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo) 
        { 
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            var data = await _userRepo.GetAllAsync(Convert.ToString(email));

            return Ok(new {data = data, message = "Get all data users"});
        }
    }
}
