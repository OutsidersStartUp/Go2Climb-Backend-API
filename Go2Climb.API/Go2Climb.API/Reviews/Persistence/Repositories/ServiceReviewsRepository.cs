using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Go2Climb.API.Persistence.Repositories
{
    public class ServiceReviewsRepository : BaseRepository, IServiceReviewRepository
    {
        public ServiceReviewsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ServiceReview>> ListAsync()
        {
            return await _context.ServiceReviews.ToListAsync();
        }

        public async Task<IEnumerable<ServiceReview>> ListByServiceId(int serviceId)
        {
            return await _context.ServiceReviews.Where(p => p.ServiceId == serviceId).Include(p => p.Customer).ToListAsync();        }

        public async Task<IEnumerable<ServiceReview>> ListByCustomerId(int customerId)
        {
            return await _context.ServiceReviews.Where(p => p.CustomerId == customerId).Include(p => p.Customer).ToListAsync();
        }

        public async Task AddAsync(ServiceReview serviceReview)
        {
            await _context.ServiceReviews.AddAsync(serviceReview);
        }

        public async Task<ServiceReview> FindByIdAsync(int id)
        {
            return await _context.ServiceReviews
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Remove(ServiceReview serviceReview)
        {
            _context.ServiceReviews.Remove(serviceReview);
        }
    }
}