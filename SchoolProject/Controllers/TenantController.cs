using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepo;
        public TenantController(ITenantRepository tenantRepo)
        {
            _tenantRepo = tenantRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tenants = await _tenantRepo.GetAllTenantsAsync();
            return Ok(new {data = tenants , message = "Get all data tenants"});
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] TenantDTO req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Name) || string.IsNullOrEmpty(req.Name))
                {
                    return BadRequest("Name is required");
                }
                var existingTenant = await _tenantRepo.GetTenantByNameAsycn(req.Name);
                if (existingTenant != null)
                {
                    return Conflict("Tenant name already exists.");
                }

                var tenant = new Tenant(req.Name);

                _tenantRepo.AddTenantAsync(tenant);

                return Ok(new {data = tenant , message = "Success create data tenant"});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
