using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveServiceReviewResource
    {
        [Required]
        public int ServiceId { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public string Date { get; set; }
        
        [Required(ErrorMessage = "Comment is required")]
        [MaxLength(200)]
        public string Comment { get; set; }
        
        [Required]
        [Range(0, 5)]
        public double Score { get; set; }
    }
}