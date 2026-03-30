namespace SchoolProject.Domain.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }

        public DayOfWeek Day;
        public TimeSpan StartTime;
        public TimeSpan EndTime;

        public Class Class { get; set; }
        public Subject Subject { get; set; }
    }
}
