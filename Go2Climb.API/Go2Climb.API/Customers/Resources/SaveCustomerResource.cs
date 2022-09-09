using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveCustomerResourse
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
    }
}