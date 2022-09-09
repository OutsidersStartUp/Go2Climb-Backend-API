namespace Go2Climb.API.Domain.Models
{
    public class HiredService
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string ScheduledDate { get; set; }
        public string Status { get; set; }
        
        // Relationships
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}