using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> ListAsync();
        Task<IEnumerable<Service>> ListByAgencyId(int agencyId);
        Task<IEnumerable<Service>> ListByText(string name, int start, int limit);
        Task<IEnumerable<Service>> ListByTextFilterMoney(string name, int min, int max, int start, int limit);
        Task<IEnumerable<Service>> ListByTextFilterScore(string name, int score, int start, int limit);
        Task<IEnumerable<Service>> ListByTextAndAllFilter(string name, int score, int min, int max, int start, int limit);
        Task<IEnumerable<Service>> FilterByCategoryOffers(int start, int limit);
        Task<IEnumerable<Service>> FilterByCategoryPopulars(int start, int limit);
        Task<IEnumerable<Service>> FilterByCategoryForYou(int start, int limit);
        Task<IEnumerable<Service>> ListById(int id);
        Task<Service> FindById(int id);
        Task AddAsync(Service service);
        void Update(Service service);
        void Remove(Service service);
    }
}