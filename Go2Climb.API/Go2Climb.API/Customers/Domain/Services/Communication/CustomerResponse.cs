using System.Runtime.CompilerServices;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Resources;

namespace Go2Climb.API.Domain.Services.Communication
{
    public class CustomerResponse : BaseResponse<Customer>
    {
        
        public CustomerResponse(Customer customer) : base(customer) {}

        public CustomerResponse(string message) : base(message) {}
    }
}