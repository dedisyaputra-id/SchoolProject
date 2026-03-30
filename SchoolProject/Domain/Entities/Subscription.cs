using System.Runtime.InteropServices;

namespace SchoolProject.Domain.Entities
{
    public class Subscription
    {
        public Guid id { get; set; }
        public Guid TenantId { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Tenant Tenant { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
