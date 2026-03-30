namespace SchoolProject.Domain.Entities
{
    public class SubscriptionPlan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MaxStudents { get; set; }
        public bool HasPremiumFeature { get; set; }
    }
}
