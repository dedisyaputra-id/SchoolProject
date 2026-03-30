using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;
        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> Get(Guid tenantId)
        {
            try
            {
                var enrollments = await _context.Enrollments.Where(e => e.TenantId == tenantId).ToListAsync();
                return enrollments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching enrollments: {ex.Message}");
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<Enrollment> GetById(Guid id)
        {
            try
            {
                var enrollment = await _context.Enrollments.FindAsync(id);
                return enrollment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching the enrollment by ID: {ex.Message}");
                throw;
            }
        }
        [HttpPost]
        public async Task<Enrollment> Create(Enrollment enrollment)
        {
            try
            {
                var enrollmentEntiry = await _context.AddAsync(enrollment);
                await _context.SaveChangesAsync();
                return enrollment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the enrollment: {ex.Message}");
                throw;
            }

        }
    }
}
