using Airtraffic.Domain.Models;
using Airtraffic.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airtraffic.Persistence.Repositories;

namespace Airtraffic.Domain.Repositories
{
    public class AircraftRepository<T> : BaseRepository, IAircraftRepository<T> where T : Aircraft
    {
        public AircraftRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IList<T>> ReadAll()
        {
            return await _context.Set<T>().Include(a => a.CurrentAirport).ToListAsync();
        }
        public async Task<T> Read(int id)
        {
            return (T)await _context.Aircrafts.Include(a => a.CurrentAirport)
                                          .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _context.Aircrafts.Remove(entity);
        }
    }
    public class AirplaneRepository : AircraftRepository<Airplane>
    {
        public AirplaneRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class GliderRepository : AircraftRepository<Glider>
    {
        public GliderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
