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
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubcriptionPlan _subscriptionPlanService;
        public SubscriptionPlanController(ISubcriptionPlan subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }
        [Authorize(Roles = "superadmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSubscriptionPlans()
        {
            var plans = await _subscriptionPlanService.GetAllSubscriptionPlans();
            return Ok(new { data = plans, message = "Get all subscription plans success" });
        }
        [Authorize(Roles = "superadmin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubscriptionPlanById(Guid id)
        {
            var plan = await _subscriptionPlanService.GetSubscriptionPlanById(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(new { data = plan, message = "Get data success" });
        }
        [Authorize(Roles = "superadmin")]
        [HttpPost]
        public async Task<IActionResult> CreateSubscriptionPlan(SubscriptionPlanDTO req)
        {
            try
            {
                var existingPlan = await _subscriptionPlanService.GetSubscriptionPlanByName(req.Name);
                if (existingPlan != null)
                {
                    return BadRequest(new { message = "Subscription plan with the same name already exists" });
                }
                
                var SubscriptionPlan = new SubscriptionPlan
                {
                    Name = req.Name,
                    Description = req.Description,
                    Price = req.Price,
                    MaxStudents = req.MaxStudents,
                    HasPremiumFeature = req.HasPremiumFeature
                };

                var createdPlan = await _subscriptionPlanService.CreateSubscriptionPlan(SubscriptionPlan);


                return Ok(new { data = createdPlan, message = "Create subscription plan success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
