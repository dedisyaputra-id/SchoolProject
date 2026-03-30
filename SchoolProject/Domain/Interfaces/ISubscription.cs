using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface ISubscription
    {
        Task<List<Subscription>> Get();
        Task<Subscription> GetById(Guid id);
        Task<Subscription> GetByTenantId(Guid tenantId); 
        Task<Subscription> Create(Subscription subscription);
        //Task<Subscription> Update(Guid id, Subscription subscription);
        //Task<bool> Delete(Guid id);
    }
}
