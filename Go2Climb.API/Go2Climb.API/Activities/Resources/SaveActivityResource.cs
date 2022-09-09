using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveActivityResource
    {
        
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}