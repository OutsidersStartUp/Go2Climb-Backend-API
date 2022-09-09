using System.Threading.Tasks;

namespace Go2Climb.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}