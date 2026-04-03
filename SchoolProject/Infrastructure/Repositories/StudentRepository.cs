using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Helper;
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

        public async Task<PagedResult<Student>> GetAllStudent(Guid tenantId, PaginationParams param)
        {
            var query = _context.Students.Where(s => s.TenantId == tenantId).AsQueryable();
            var result = await query.ToPagedResultAsync(param);
            return result;
        }

        public async Task<Student> GetByIdAsycn(Guid id, Guid tenantId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.TenantId == tenantId);
        }
    }
}
