using SchoolProject.Domain.Entities;
using SchoolProject.Infrastructure.Persistance;

namespace SchoolProject.Data.Seeder
{
    public static class StudentSeeder
    {
        public static async Task SeedStudents(AppDbContext context)
        {
            if (context.Students.Any()) return;

            var students = new List<Student>();

            for (int i = 1; i <= 100000; i++)
            {
                students.Add(new Student($"Student {i}", $"student{i}@mail.com", Guid.Parse("58AE3477-9C8A-4A39-A9C6-79507360C35D")));
            }

            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }
    }
}
