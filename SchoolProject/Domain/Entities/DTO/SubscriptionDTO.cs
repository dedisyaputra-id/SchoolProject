namespace SchoolProject.Domain.Entities.DTO
{
    public class SubscriptionDTO
    {
        public Guid SubscriptionPlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
