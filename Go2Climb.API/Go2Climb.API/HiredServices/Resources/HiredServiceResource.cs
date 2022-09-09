namespace Go2Climb.API.Resources
{
    public class HiredServiceResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string ScheduledDate { get; set; }
        public string Status { get; set; }
        public CustomerResource Customer { get; set; }
        public ServiceResource Service { get; set; }
    }
}