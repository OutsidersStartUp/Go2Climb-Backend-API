using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveAgencyResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Location { get; set; }
        
        [Required]
        public string Ruc { get; set; }
    }
}