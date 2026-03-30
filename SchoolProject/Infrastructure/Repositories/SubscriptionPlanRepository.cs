using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubscriptionPlanRepository : ISubcriptionPlan
    {
        private readonly AppDbContext _context;
        public SubscriptionPlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<SubscriptionPlan> CreateSubscriptionPlan(SubscriptionPlan subscriptionPlan)
        {
            _context.SubscriptionPlans.Add(subscriptionPlan);
            _context.SaveChanges();
            return Task.FromResult(subscriptionPlan);
        }
        public Task<SubscriptionPlan> GetSubscriptionPlanById(Guid id)
        {
            var plan = _context.SubscriptionPlans.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(plan);
        }
        public Task<SubscriptionPlan> GetSubscriptionPlanByName(string name)
        {
            var plan = _context.SubscriptionPlans.FirstOrDefault(p => p.Name == name);
            return Task.FromResult(plan);
        }

        public Task<IEnumerable<SubscriptionPlan>> GetAllSubscriptionPlans()
        {
            var plans = _context.SubscriptionPlans.ToList();
            return Task.FromResult(plans.AsEnumerable());
        }

    }
}
