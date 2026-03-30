using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscription _subscription;
        public SubscriptionController(ISubscription subscription)
        {
            _subscription = subscription;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscription()
        {
            var subscription = await _subscription.Get();
            return Ok(new {data = subscription, message ="get all data success"});
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscriptionById(Guid id)
        {
            var subscription = await _subscription.GetById(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubscription(SubscriptionDTO req)
        {
            try
            {
                var tenantId = HttpContext?.User?.FindFirst("tenantId")?.Value;

                var subscription = new Subscription
                {
                    id = Guid.NewGuid(),
                    SubscriptionPlanId = req.SubscriptionPlanId,
                    StartDate = req.StartDate,
                    EndDate = req.EndDate,
                    TenantId = Guid.Parse(tenantId ?? throw new Exception("Tenant ID not found in token"))
                };

                var createdSubscription = await _subscription.Create(subscription);

                return Ok(createdSubscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
