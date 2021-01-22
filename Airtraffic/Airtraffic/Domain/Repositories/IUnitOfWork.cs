using System.Threading.Tasks;

namespace Airtraffic.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
