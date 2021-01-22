using Airtraffic.Domain.Models;
using Airtraffic.Domain.Repositories;
using Airtraffic.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airtraffic.Persistence.Repositories
{
    public class AirportRepository : BaseRepository, IAirportRepository
    {
        public AirportRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Airport airport)
        {
            await _context.Airports.AddAsync(airport);
        }

        public async Task<Airport> FindByIdAsync(int id)
        {
            return await _context.Airports.FindAsync(id);
        }

        public async Task<IEnumerable<Airport>> ListAsync()
        {
            return await _context.Airports.ToListAsync();
        }

        public void Update(Airport airport)
        {
            _context.Airports.Update(airport);
        }

        public void Remove(Airport airport)
        {
            _context.Airports.Remove(airport);
        }
    }
}
