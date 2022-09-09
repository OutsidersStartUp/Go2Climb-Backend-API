namespace Go2Climb.API.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        //public int AgencyId { get; set; }
        //public Agency Agency { get; set; }
    }
}