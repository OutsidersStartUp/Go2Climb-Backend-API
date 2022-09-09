using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveAgencyReviewResource
    {
        [Required]
        public int AgencyId { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public string Date { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Comment { get; set; }
        
        [Required]
        [Range(0, 5)]
        public double ProfessionalismScore { get; set; }
        
        [Required]
        [Range(0, 5)]
        public double SecurityScore { get; set; }
        
        [Required]
        [Range(0, 5)]
        public double QualityScore { get; set; }
        
        [Required]
        [Range(0, 5)]
        public double CostScore { get; set; }
    }
}