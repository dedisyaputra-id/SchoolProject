namespace SchoolProject.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }

        // Multitenant
        public Guid TenantId { get; set; }

        // Informasi subject
        public string Name { get; set; } // "Matematika"
        public string Code { get; set; } // "MTK"
        public int CreditHours { get; set; } // jam pelajaran

        // Opsional
        public Guid? MajorId { get; set; } // kalau subject khusus jurusan

        // Navigation
        public ICollection<Schedule> Schedules { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public Major Major { get; set; }
    }
}
