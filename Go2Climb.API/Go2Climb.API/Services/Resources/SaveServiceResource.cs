using System.ComponentModel.DataAnnotations;

namespace Go2Climb.API.Resources
{
    public class SaveServiceResource
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        public short Score { get; set; }
        
        [Required]
        public int Price { get; set; }
        
        public int NewPrice { get; set; }
        
        [Required]
        public string Location { get; set; }
        
        [Required]
        public string CreationDate { get; set; }
        
        public string Photos { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        
        public bool IsOffer { get; set; }
        
        [Required]
        public int AgencyId { get; set; }
    }
}