using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _repo;
        public EnrollmentController(IEnrollmentRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                var enrollments = await _repo.Get(Guid.Parse(tenantId));
                return Ok(new { data = enrollments, message = "Get all data enrollments" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                var enrollment = await _repo.GetById(id);
                if (enrollment == null)
                {
                    return NotFound("Enrollment not found");
                }
                return Ok(new { data = enrollment, message = "Get data enrollment by id" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(EnrollmentDTO req)
        {
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                var enrollment = new Enrollment
                {
                    Id = Guid.NewGuid(),
                    AcademicYear = req.AcademicYear,
                    Semester = req.Semester,
                    Status = req.Status,
                    EnrolledAt = DateTime.UtcNow,
                    TenantId = Guid.Parse(tenantId),
                    StudentId = Guid.Parse("05ea84d1-b3af-4b9b-83e5-8b926f2b8a84"),
                    ClassId = Guid.Parse("F24F0F90-ABBF-4E33-9CED-88C28EC96691")
                };
                var createdEnrollment = await _repo.Create(enrollment);
                return Ok(new { data = createdEnrollment, message = "Enrollment created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
