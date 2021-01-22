using Airtraffic.Domain.Models;
using Airtraffic.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airtraffic.Domain.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<Airport>> ListAsync();
        Task<AirportResponseObject> CreateAsync(Airport airport);
        Task<AirportResponseObject> UpdateAsync(int id, Airport airport);
        Task<AirportResponseObject> DeleteAsync(int id);
    }
}