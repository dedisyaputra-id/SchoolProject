using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> Get(Guid tenantId);
        Task<Class> GetById(Guid id);
        Task<Class> Create(Class classes);
    }
}
