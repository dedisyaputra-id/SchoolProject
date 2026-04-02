using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Repositories;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace SchoolProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ISubscription _subscription;
        private readonly ILogger<StudentController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _cache;
        public StudentController(IStudentRepository studentRepo, ISubscription subcription, ILogger<StudentController> logger, IServiceProvider serviceProvider, IMemoryCache cache)
        {
            _studentRepo = studentRepo;
            _subscription = subcription;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _cache = cache;
        }

        [Authorize(Roles = "admin,teacher")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;
                string cacheKey = $"students_{tenantId}";

                _logger.LogInformation("Getting all students with tenantId : {TenantId}", tenantId);

                if(!_cache.TryGetValue(cacheKey, out List<Student> data))
                {
                    data = await _studentRepo.GetAllStudent(Guid.Parse(tenantId));

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    _cache.Set(cacheKey, data, cacheOptions);
                }

                return Ok(new { data = data, message = "Get all data student" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting students");
                return StatusCode(500, new { message = "An error occurred while getting students", error = ex.Message });
            }
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
                var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var subscription = await _subscription.GetByTenantId(Guid.Parse(tenantId));

                if (subscription == null)
                {
                    return BadRequest(new { message = "Subscription not found for the tenant, cannot insert data student" });
                }

                var student = new Student(req.Name, req.Email, Guid.Parse(tenantId));

                var createdStudent = await _studentRepo.AddAsycn(student);

                _logger.LogInformation("User {UserId} create student {StudentId} in Tenant {TenantId}", userId, student.Id.ToString(), tenantId);

                return Ok(new { data = createdStudent, message = "Student created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the student");
                return StatusCode(500, new { message = "An error occurred while creating the student", error = ex.Message });
            }
        }
        [HttpPost("export")]
        public async Task<IActionResult> ExportStudents()
        {
            var tenantId = Guid.Parse(HttpContext?.User?.FindFirst("tenantId")?.Value);
            using var scope = _serviceProvider.CreateScope();
            var exportService = scope.ServiceProvider.GetRequiredService<StudentExportRepository>();

            BackgroundJob.Enqueue(() => exportService.ExportStudentToExcel(tenantId));

            return Ok(new { message = "Student export job has been queued" });
        }
    }
}
