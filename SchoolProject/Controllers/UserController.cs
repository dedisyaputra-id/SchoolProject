using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo) 
        { 
            _userRepo = userRepo;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var data = _userRepo.GetAllAsync();
            return Ok(new {data = data, message = "Get all data users"});
        }
    }
}
