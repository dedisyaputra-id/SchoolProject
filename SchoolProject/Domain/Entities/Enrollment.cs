using System.Security.Claims;

namespace SchoolProject.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }

        // Multitenant
        public Guid TenantId { get; set; }

        // Relasi
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }

        // Akademik
        public string AcademicYear { get; set; }
        public int Semester { get; set; }

        // Status
        public string Status { get; set; } // Active, Completed, Dropped

        // Tracking
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Navigation (penting untuk EF Core)
        public Student Student { get; set; }
        public Class Class { get; set; }
    }
}
