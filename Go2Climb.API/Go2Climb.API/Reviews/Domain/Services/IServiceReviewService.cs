using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace Go2Climb.API.Domain.Services
{
    public interface IServiceReviewService
    {
        Task<IEnumerable<ServiceReview>> ListAsync();
        Task<IEnumerable<ServiceReview>> ListByServiceIdAsync(int serviceId);
        Task<IEnumerable<ServiceReview>> ListByCustomerIdAsync(int customerId);
        Task<ServiceReviewResponse> GetByIdAsync(int id);
        Task<ServiceReviewResponse> SaveAsync(ServiceReview serviceReview);
        Task<ServiceReviewResponse> DeleteAsync(int id);
    }
}