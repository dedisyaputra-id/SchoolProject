namespace SchoolProject.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }

        // Multitenant
        public Guid TenantId { get; set; }

        // Informasi dasar
        public string Name { get; set; }
        public string Email { get; set; }

        // Opsional (kalau ada sistem kepegawaian)
        public string EmployeeNumber { get; set; }

        // Navigation

        // Guru bisa jadi wali kelas di beberapa kelas
        public ICollection<Class> HomeroomClasses { get; set; }

        // Guru mengajar banyak jadwal
        public ICollection<Schedule> Schedules { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
    }
}
