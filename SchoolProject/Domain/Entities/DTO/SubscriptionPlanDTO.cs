namespace SchoolProject.Domain.Entities.DTO
{
    public class SubscriptionPlanDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MaxStudents { get; set; }
        public bool HasPremiumFeature { get; set; }
    }
}
