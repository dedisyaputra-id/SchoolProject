namespace SchoolProject.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Tenant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
