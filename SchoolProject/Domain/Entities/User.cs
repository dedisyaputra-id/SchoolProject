namespace SchoolProject.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public Guid? TenantId { get; set; }

        public User() { }
        public User(string name, string email, string role, string passwordHash, Guid tenantId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Role = role;
            PasswordHash = passwordHash;
            TenantId = tenantId;
        }
    }
}
