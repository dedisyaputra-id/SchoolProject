using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> Get(Guid tenantId);
        Task<Enrollment> GetById(Guid id);
        Task<Enrollment> Create(Enrollment enrollment);
    }
}
