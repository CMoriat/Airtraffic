using Airtraffic.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airtraffic.Domain.Repositories
{
    public interface IAircraftRepository<T> where T : Aircraft
    {
        Task<IList<T>> ReadAll();
        Task<T> Read(int id);
        Task Create(T entity);  
        void Update(T entity);
        void Delete(T entity);
    }
}