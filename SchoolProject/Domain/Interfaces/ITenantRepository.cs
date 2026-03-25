using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant> GetTenantByIdAsync(Guid tenantId);
        Task<Tenant> GetTenantByNameAsycn(string name);
        Task<IEnumerable<Tenant>> GetAllTenantsAsync();
        void AddTenantAsync(Tenant tenant);
        void UpdateTenantAsync(Tenant tenant);
        void DeleteTenantAsync(Guid tenantId);
    }
}
