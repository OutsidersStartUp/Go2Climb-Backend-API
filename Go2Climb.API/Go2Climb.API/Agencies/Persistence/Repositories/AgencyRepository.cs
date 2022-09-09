using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Go2Climb.API.Persistence.Repositories
{
    public class AgencyRepository : BaseRepository, IAgencyRepository
    {
        public AgencyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Agency>> ListAsync()
        {
            return await _context.Agencies.ToListAsync();
        }
        
        public async Task AddAsync(Agency agency)
        {
            await _context.Agencies.AddAsync(agency);
        }

        public async Task<Agency> FindByIdAsync(int id)
        {
            return await _context.Agencies.FindAsync(id);
        }

        public Agency FindById(int id)
        {
            return _context.Agencies.Find(id);
        }
        
        public async Task<Agency> FindByEmailAsync(string email)
        {
            return await _context.Agencies.SingleOrDefaultAsync(C => C.Email == email);
        }
        
        public bool ExistsByEmail(string email)
        {
            return _context.Agencies.Any(c => c.Email == email);
        }

        public async Task<IEnumerable<Agency>> ListByName(string name)
        {
            return await _context.Agencies.Where(p => p.Name == name).ToListAsync();
        }

        public void Update(Agency agency)
        {
            _context.Agencies.Update(agency);
        }

        public void Remove(Agency agency)
        {
            _context.Agencies.Remove(agency);
        }
    }
}