using System.Threading.Tasks;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;

namespace Go2Climb.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}