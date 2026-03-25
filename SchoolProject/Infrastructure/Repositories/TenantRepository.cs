using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;
        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            var tenants = _context.Tenants.ToList();
            return Task.FromResult(tenants.AsEnumerable());
        }

        public Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            var tenant = _context.Tenants.FirstOrDefault(t => t.Id == tenantId);
            return Task.FromResult(tenant);
        }

        public Task<Tenant> GetTenantByNameAsycn(string name)
        {
            var tenant = _context.Tenants.FirstOrDefault(t => t.Name == name);
            return Task.FromResult(tenant);
        }

        public void AddTenantAsync(Tenant tenant)
        {
            try
            {
                _context.AddAsync(tenant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred while add the tenant: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public void UpdateTenantAsync(Tenant tenant)
        {
            try
            {
                _context.Tenants.Update(tenant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred while updating the tenant: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public void DeleteTenantAsync(Guid tenantId)
        {
            var tenant = _context.Tenants.FirstOrDefault(t => t.Id == tenantId);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                _context.SaveChanges();
            }
        }
    }
}
