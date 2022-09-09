using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Go2Climb.API.Persistence.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Activity>> ListAsync()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<IEnumerable<Activity>> ListById(int id)
        {
            return await _context.Activities.Where(p => p.Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Activity>> ListByServiceId(int serviceId)
        {
            return await _context.Activities.Where(p => p.ServiceId == serviceId).Include(p => p.Service).ToListAsync();
        }

        public async Task<Activity> FindById(int id)
        {
            return await _context.Activities.FindAsync(id);
        }

        public async Task AddAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
        }

        public void Update(Activity activity)
        {
            _context.Activities.Update(activity);
        }

        public void Remove(Activity activity)
        {
            _context.Activities.Remove(activity);
        }
    }
}