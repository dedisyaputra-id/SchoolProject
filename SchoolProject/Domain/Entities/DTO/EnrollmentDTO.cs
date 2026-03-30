using System.Security.Claims;

namespace SchoolProject.Domain.Entities.DTO
{
    public class EnrollmentDTO
    {
        // Akademik
        public string AcademicYear { get; set; }
        public int Semester { get; set; }

        // Status
        public string Status { get; set; } // Active, Completed, Dropped

        // Tracking
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
