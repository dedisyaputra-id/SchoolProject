using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface ISubcriptionPlan
    {
        Task<SubscriptionPlan> CreateSubscriptionPlan(SubscriptionPlan subscriptionPlan);
        Task<SubscriptionPlan> GetSubscriptionPlanById(Guid id);
        Task<SubscriptionPlan> GetSubscriptionPlanByName(string name);
        Task<IEnumerable<SubscriptionPlan>> GetAllSubscriptionPlans();
        //Task<SubscriptionPlan> UpdateSubscriptionPlan(Guid id, SubscriptionPlan subscriptionPlan);
        //Task<bool> DeleteSubscriptionPlan(Guid id);
    }
}
