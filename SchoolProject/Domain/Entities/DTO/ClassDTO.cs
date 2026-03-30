namespace SchoolProject.Domain.Entities.DTO
{
    public class ClassDTO
    {
        // Informasi kelas
        public string Name { get; set; } // contoh: "X IPA 1"
        public string GradeLevel { get; set; } // contoh: "10", "11", "12"

        // Akademik
        public string AcademicYear { get; set; } // "2025/2026"
        public int Semester { get; set; }

        // Relasi
        //public Guid? HomeroomTeacherId { get; set; }

        //// Navigation
        //public ICollection<Enrollment> Enrollments { get; set; }
        //public ICollection<Schedule> Schedules { get; set; }

        //// Audit
        //public DateTime CreatedAt { get; set; }
    }
}
