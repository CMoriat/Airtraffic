using Airtraffic.Domain.Models;
using Airtraffic.Domain.Repositories;
using Airtraffic.Domain.Services;
using Airtraffic.Domain.Services.Communication;
using Airtraffic.DTOs;
using Airtraffic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airtraffic.Services
{
    public class AircraftService<T> : IAircraftService<T> where T : Aircraft
    {
        private readonly IAircraftRepository<T> _aircraftRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAirportRepository _airportRepository;

        public AircraftService(IAircraftRepository<T> aircraftRepository, IUnitOfWork unitOfWork, IAirportRepository airportRepository)
        {
            _aircraftRepository = aircraftRepository;
            _unitOfWork = unitOfWork;
            _airportRepository = airportRepository;
        }
        public async Task<AircraftResponseObject<T>> CreateAsync(T aircraft)
        {
            try
            {
                await _aircraftRepository.Create(aircraft);
                await _unitOfWork.CompleteAsync();

                return new AircraftResponseObject<T>(aircraft);
            }
            catch (Exception e)
            {
                return new AircraftResponseObject<T>($"The following error occured when creating the aircraft: {e.Message}");
            }
        }

        public async Task<IList<T>> ListAllAircrafts()
        {
            return await _aircraftRepository.ReadAll();
        }

        public async Task<AircraftResponseObject<T>> FindById(int id)
        {
            try
            {
                var aircraft = await _aircraftRepository.Read(id);
                if (aircraft == null) return new AircraftResponseObject<T>($"No aircraft found with id:  {id}");
                return new AircraftResponseObject<T>(aircraft);
            }
            catch (Exception e)
            {
                return new AircraftResponseObject<T>($"The following error occured when locating the aircraft: {e.Message}");
            }
        }

        public async Task<AircraftResponseObject<T>> DeleteAsync(int id)
        {
            var aircraftToDelete = await _aircraftRepository.Read(id);
            try
            {
                _aircraftRepository.Delete(aircraftToDelete);
                await _unitOfWork.CompleteAsync();

                return new AircraftResponseObject<T>(aircraftToDelete);
            }
            catch (Exception e)
            {
                return new AircraftResponseObject<T>($"The following error occured when deleting the aircraft: {e.Message}");
            }
        }

        public async Task<AircraftResponseObject<T>> UpdateAsync(int id, T aircraft)
        {
            aircraft.Id = id;
            try
            {
                _aircraftRepository.Update(aircraft);
                await _unitOfWork.CompleteAsync();

                return new AircraftResponseObject<T>(aircraft);
            }
            catch (Exception e)
            {
                return new AircraftResponseObject<T>($"The following error occured when updating the aircraft: {e.Message}");
            }
        }

        public async Task<FlightResponse> Fly(int id, int destinationId)
        {
            var aircraft = await _aircraftRepository.Read(id);
            if (aircraft == null) return new FlightResponse("Aircraft not found");
            if (aircraft.AirportId == destinationId) return new FlightResponse("Aircraft is already at destination");
            var destinationAirport = await _airportRepository.FindByIdAsync(destinationId);
            if (destinationAirport == null) return new FlightResponse("Destination airport not found");

            var flightInformation = aircraft.Fly(destinationAirport);

            aircraft.AirportId = destinationId;
            try
            {
                _aircraftRepository.Update(aircraft);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new FlightResponse($"The following error occured in air: {e.Message}");
            }
            return new FlightResponse(flightInformation);
        }

    }

    public class GliderCrudService : AircraftService<Glider>
    {

        public GliderCrudService(IAircraftRepository<Glider> aircraftRepository, 
                                 IUnitOfWork unitOfWork,
                                 IAirportRepository airportRepository)
                : base(aircraftRepository, unitOfWork, airportRepository)
        {
        }
    }

    public class AirplaneCrudService : AircraftService<Airplane>
    {
        public AirplaneCrudService(IAircraftRepository<Airplane> aircraftRepository, 
                                   IUnitOfWork unitOfWork, 
                                   IAirportRepository airportRepository)
               : base(aircraftRepository, unitOfWork, airportRepository)
        {
        }
    }
}
