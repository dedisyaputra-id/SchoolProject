using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public Task<List<User>> GetAllAsync(string email)
        {
            var users = _context.Users.Where(u => u.Email != email).ToList();
            return Task.FromResult(users);
        }

        public void AddAsync(User user)
        {
            try
            {
                _context.AddAsync(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred while adding the user: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        public void UpdateAsync(User user)
        {
            _context.Users.Update(user);
        }
    }
}
