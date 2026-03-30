using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _repo;
        public TeacherController(ITeacherRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                var teachers = await _repo.Get(Guid.Parse(tenantId));
                return Ok(new { data = teachers, message = "Get all data teachers" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeacherDTO req)
        {
            try
            {
                if(string.IsNullOrEmpty(req.Name) || string.IsNullOrWhiteSpace(req.Name))
                    return BadRequest("Name is required");
                if (string.IsNullOrEmpty(req.Email) || string.IsNullOrWhiteSpace(req.Email))
                    return BadRequest("Email is required");

                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                var employeNumber = await GenerateEmployeeNumberAsync();

                var teacher = new Teacher
                {
                    Id = Guid.NewGuid(),
                    Name = req.Name,
                    Email = req.Email,
                    TenantId = Guid.Parse(tenantId),
                    EmployeeNumber = employeNumber,
                    CreatedAt = DateTime.Now,
                };

                await _repo.Create(teacher);

                return Ok(new { data = teacher });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private async Task<string> GenerateEmployeeNumberAsync()
        {
            var year = DateTime.UtcNow.Year;
            var random = new Random();

            string employeeNumber;
            bool exists;

            do
            {
                var number = random.Next(1000, 9999); // 4 digit
                employeeNumber = $"EMP-{year}-{number}";

                exists = await _repo.GetByEmployeNumber(employeeNumber);
            }
            while (exists);

            return employeeNumber;
        }
    }
}
