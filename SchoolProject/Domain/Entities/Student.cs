namespace SchoolProject.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid TenantId { get; set; }

        public Student(Guid id, string name, string email, Guid tenantId)
        {
            Id = id;
            Name = name;
            Email = email;
            TenantId = tenantId;
        }

        public void UpdateName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

    }
}
