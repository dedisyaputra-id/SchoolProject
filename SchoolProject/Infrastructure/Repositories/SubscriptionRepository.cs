using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscription
    {
        private readonly AppDbContext _context;
        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Subscription>> Get()
        {
            var subscription = _context.Subscriptions.ToList();
            return Task.FromResult(subscription);
        }
        public Task<Subscription> GetById(Guid id)
        {
            var subscription = _context.Subscriptions.FirstOrDefault(s => s.id == id);
            return Task.FromResult(subscription);
        }
        public Task<Subscription> GetByTenantId(Guid tenantId)
        {
            var subscription = _context.Subscriptions.FirstOrDefault(s => s.TenantId == tenantId);
            return Task.FromResult(subscription);
        }
        public Task<Subscription> Create(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
            return Task.FromResult(subscription);
        }
    }
}
