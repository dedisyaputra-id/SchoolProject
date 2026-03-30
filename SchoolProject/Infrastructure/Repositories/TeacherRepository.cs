using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> Get(Guid tenantId)
        {
            try
            {
                var teacher = await _context.Teachers.Where(t => t.TenantId == tenantId).ToListAsync();
                return teacher;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Teacher> GetByEmail(string email)
        {
            try
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
                return teacher;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> GetByEmployeNumber(string number)
        {
            try
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.EmployeeNumber == number);
                if (teacher != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Teacher> Create(Teacher teacher)
        {
            try
            {
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return teacher;
            }
            catch (Exception ex)
            { 
                throw;
            }
        }

        public async Task<Teacher> Update(Teacher teacher)
        {
            try
            {
                _context.Teachers.Update(teacher);
                _context.SaveChanges();
                return teacher;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
