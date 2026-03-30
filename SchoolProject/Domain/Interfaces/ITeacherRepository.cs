using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> Get(Guid tenantId);
        Task<Teacher> GetByEmail(string email);
        Task<Teacher> Create(Teacher teacher);
        Task<Teacher> Update(Teacher teacher);
        Task<bool> GetByEmployeNumber(string number);

    }
}
