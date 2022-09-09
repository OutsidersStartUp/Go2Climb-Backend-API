namespace Go2Climb.API.Security.Domain.Services.Communication
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Photo { get; set; }
    }
}