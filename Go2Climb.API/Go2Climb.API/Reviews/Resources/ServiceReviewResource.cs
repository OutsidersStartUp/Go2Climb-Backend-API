namespace Go2Climb.API.Resources
{
    public class ServiceReviewResource
    {
        public int Id { get; set; }
        public int ServiceId { get; set; } 
        public int CustomerId { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; } 
        public double Score { get; set; }
        public ServiceResource Service { get; set; }
        public CustomerResource Customer { get; set; }
    }
}