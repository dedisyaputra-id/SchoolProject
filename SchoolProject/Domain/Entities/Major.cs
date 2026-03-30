namespace SchoolProject.Domain.Entities
{
    public class Major
    {
        public Guid Id { get; set; }

        // Multitenant
        public Guid TenantId { get; set; }

        // Informasi jurusan
        public string Name { get; set; } // "IPA", "RPL"
        public string Code { get; set; } // "IPA", "RPL"
        public string Description { get; set; }

        // Navigation
        public ICollection<Class> Classes { get; set; }
        public ICollection<Subject> Subjects { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
    }
}
