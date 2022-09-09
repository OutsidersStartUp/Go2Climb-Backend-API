using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> ListAsync();
        Task<IEnumerable<Subscription>> ListById(int id);
        Task AddAsync(Subscription subscription);
        void Update(Subscription subscription);
        void Remove(Subscription subscription);
        Task<Subscription> FindById(int id);
    }
}