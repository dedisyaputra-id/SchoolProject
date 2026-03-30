using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;
        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> Get(Guid tenantId)
        {
            try
            {
                var classes = await _context.Class.Where(c => c.TenantId == tenantId).ToListAsync();
                return classes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching classes: {ex.Message}");
                throw;
            }
        }
        public async Task<Class> GetById(Guid id)
        {
            try
            {
                var classEntity = await _context.Class.FindAsync(id);
                return classEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the class by ID: {ex.Message}");
                throw;
            }
        }
        public async Task<Class> Create(Class clasess)
        {
            try
            {
                var createdClass = await _context.Class.AddAsync(clasess);
                await _context.SaveChangesAsync();
                return createdClass.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the class: {ex.Message}");
                throw;
            }
        }
    }
}
