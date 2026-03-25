using Microsoft.Identity.Client;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Implement a simple hashing mechanism (for demonstration purposes only)
            // In production, use a stronger hashing algorithm like BCrypt or Argon2
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            // Compare the hashed password with the provided password
            return hashedPassword == HashPassword(providedPassword);
        }
    }
}
