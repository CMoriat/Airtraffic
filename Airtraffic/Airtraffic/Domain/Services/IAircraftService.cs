using Airtraffic.Domain.Models;
using Airtraffic.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airtraffic.Domain.Services
{
    public interface IAircraftService<T> where T : Aircraft
    {
        Task<IList<T>> ListAllAircrafts();   
        Task<AircraftResponseObject<T>> CreateAsync(T aircraft);
        Task<AircraftResponseObject<T>> FindById(int id);
        Task<AircraftResponseObject<T>> UpdateAsync(int id, T aircraft);
        Task<AircraftResponseObject<T>> DeleteAsync(int id);
        Task<FlightResponse> Fly(int id, int destinationId);
    }
}
