using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsycn(Guid id, Guid tenantId);
        Task<List<Student>> GetAllStudent(Guid tenantId);
        Task AddAsycn(Student student);
    }
}
