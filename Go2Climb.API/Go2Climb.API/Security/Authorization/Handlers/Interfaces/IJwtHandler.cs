using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Security.Authorization.Handlers.Interfaces
{
    public interface IJwtHandler
    {
        public string GenerateToken(Customer customer);
        public string GenerateToken(Agency agency);
        public int? ValidateToken(string token);
        
    }
}