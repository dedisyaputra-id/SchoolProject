using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> AddAsycn(Domain.Entities.Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAllStudent(Guid tenantId)
        {
            return await _context.Students.Where(s => s.TenantId == tenantId).ToListAsync();
        }

        public async Task<Student> GetByIdAsycn(Guid id, Guid tenantId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.TenantId == tenantId);
        }
    }
}
