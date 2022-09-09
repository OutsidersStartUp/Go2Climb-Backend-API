using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;

namespace Go2Climb.API.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> ListAsync();
        Task<IEnumerable<Activity>> ListById(int id);
        Task<IEnumerable<Activity>> ListByServiceId(int serviceId);
        Task<Activity> FindById(int id);
        Task AddAsync(Activity activity);
        void Update(Activity activity);
        void Remove(Activity activity);
    }
}