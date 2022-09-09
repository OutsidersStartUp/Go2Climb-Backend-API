using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveSubscriptionResource
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Price { get; set; }
    }
}