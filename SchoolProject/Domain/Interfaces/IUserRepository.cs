using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(string email);
        Task<User> GetByEmailAsync(string email);
        void AddAsync(User user);
        void UpdateAsync(User user);
    }
}
