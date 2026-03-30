namespace SchoolProject.Domain.Entities
{
    public class Class
    {
        public Guid Id { get; set; }

        // Multitenant
        public Guid TenantId { get; set; }

        // Informasi kelas
        public string Name { get; set; } // contoh: "X IPA 1"
        public string GradeLevel { get; set; } // contoh: "10", "11", "12"
        public Guid? MajorId { get; set; } // opsional (IPA, IPS, dll)

        // Akademik
        public string AcademicYear { get; set; } // "2025/2026"
        public int Semester { get; set; }

        // Relasi
        public Guid? HomeroomTeacherId { get; set; }
        public Teacher HomeroomTeacher { get; set; }

        // Navigation
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public Major Major { get; set; }
    }
}
