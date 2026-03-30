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
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _repo;
        public ClassController(IClassRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                var classes = await _repo.Get(Guid.Parse(tenantId));
                return Ok(new { data = classes, message = "Get all data classes" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var classEntity = await _repo.GetById(Guid.Parse(id));
                if (classEntity == null)
                {
                    return NotFound(new { message = "Class not found" });
                }
                return Ok(new { data = classEntity, message = "Get class by id success" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClassDTO req)
        {
            try
            {
                if(string.IsNullOrEmpty(req.Name) || string.IsNullOrWhiteSpace(req.Name))
                {
                    return BadRequest(new { message = "Name is required" });
                }
                
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;

                var classEntity = new Class
                {
                    Id = Guid.NewGuid(),
                    Name = req.Name,
                    GradeLevel = req.GradeLevel,
                    AcademicYear = req.AcademicYear,
                    Semester = req.Semester,
                    TenantId = Guid.Parse(tenantId),
                    HomeroomTeacherId = Guid.Parse("649282BF-170E-434C-A1F8-1F7E5FC1BBCA"),
                    CreatedAt = DateTime.UtcNow
                };
                var result = await _repo.Create(classEntity);
                return Ok(new { data = result, message = "Create class success" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
