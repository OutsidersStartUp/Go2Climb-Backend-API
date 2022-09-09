using Go2Climb.API.Agencies.Resources;

namespace Go2Climb.API.Resources
{
    public class AgencyReviewResource
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public int CustomerId { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
        public double ProfessionalismScore { get; set; }
        public double SecurityScore { get; set; }
        public double QualityScore { get; set; }
        public double CostScore { get; set; }
        public AgencyResource Agency { get; set; }
        public CustomerResource Customer { get; set; }
    }
}