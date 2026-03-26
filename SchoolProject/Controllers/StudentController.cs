using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        public StudentController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
            var data = await _studentRepo.GetAllStudent(Guid.Parse(tenantId));
            return Ok(new { data = data, message = "Get all data student" });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDTO req)
        {
            if (req == null)
            {
                return BadRequest(new { message = "Student data is required" });
            }
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;

                var student = new Student(req.Name, req.Email, Guid.Parse(tenantId));

                var createdStudent = await _studentRepo.AddAsycn(student);

                return Ok(new { data = createdStudent, message = "Student created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the student", error = ex.Message });
            }
        }
    }
}
