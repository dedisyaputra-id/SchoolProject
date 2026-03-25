using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        public StudentController(IStudentRepository studentRepo) 
        {
            _studentRepo = studentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Guid tenantId = Guid.NewGuid();
            var data = await _studentRepo.GetAllStudent(tenantId);
            return Ok(new {data = data, message = "Get all data student"});
        }
    }
}
