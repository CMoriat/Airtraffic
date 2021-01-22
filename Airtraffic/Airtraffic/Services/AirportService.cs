using Airtraffic.Domain.Models;
using Airtraffic.Domain.Repositories;
using Airtraffic.Domain.Services;
using Airtraffic.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airtraffic.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AirportService(IAirportRepository airportRepository, IUnitOfWork unitOfWork)
        {
            _airportRepository = airportRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AirportResponseObject> CreateAsync(Airport airport)
        {
            try
            {
                await _airportRepository.AddAsync(airport);
                await _unitOfWork.CompleteAsync();

                return new AirportResponseObject(airport);
            } 
            catch(Exception e)
            {
                return new AirportResponseObject($"The following error occured when creating the airport: {e.Message}");
            }
        }

        public async Task<IEnumerable<Airport>> ListAsync()
        {
            return await _airportRepository.ListAsync();
        }

        public async Task<AirportResponseObject> UpdateAsync(int id, Airport airport)
        {
            var airportToUpdate = await _airportRepository.FindByIdAsync(id);
            if (airportToUpdate == null) return new AirportResponseObject("Airport not found");

            airportToUpdate.Name = string.IsNullOrEmpty(airport.Name) ? airportToUpdate.Name : airport.Name;
            airportToUpdate.Longitude = airport.Longitude == 0 ? airportToUpdate.Longitude : airport.Longitude;
            airportToUpdate.Latitude = airport.Latitude == 0 ? airportToUpdate.Latitude : airport.Latitude;
            
            try
            {
                _airportRepository.Update(airportToUpdate);
                await _unitOfWork.CompleteAsync();

                return new AirportResponseObject(airportToUpdate);
            }
            catch (Exception e)
            {
                return new AirportResponseObject($"The following error occured when updating the airport: {e.Message}");
            }

        }

        public async Task<AirportResponseObject> DeleteAsync(int id)
        {
            var airportToDelete = await _airportRepository.FindByIdAsync(id);
            if (airportToDelete == null) return new AirportResponseObject("Airport not found");

            try
            {
                _airportRepository.Remove(airportToDelete);
                await _unitOfWork.CompleteAsync();

                return new AirportResponseObject(airportToDelete);
            }
            catch (Exception e)
            {
                return new AirportResponseObject($"The following error occured when deleting the airport: {e.Message}");
            }

        }
    }
}
