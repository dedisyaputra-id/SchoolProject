using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsycn(Guid id, Guid tenantId);
        Task<PagedResult<Student>> GetAllStudent(Guid tenantId, PaginationParams param);
        Task<Student> AddAsycn(Student student);
    }
}
