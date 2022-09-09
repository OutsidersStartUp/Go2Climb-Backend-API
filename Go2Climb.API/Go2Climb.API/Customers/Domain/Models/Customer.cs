using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Go2Climb.API.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public IList<AgencyReview> AgencyReviews { get; set; } = new List<AgencyReview>();
        public IList<ServiceReview> ServiceReviews { get; set; } = new List<ServiceReview>();
        public IList<HiredService> HideServices { get; set; } = new List<HiredService>();
    }
}