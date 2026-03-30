namespace SchoolProject.Domain.Entities.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        //public Guid TenanId { get; set; }
    }
}
