using Airtraffic.Domain.Models;
using AutoMapper;
using Airtraffic.DTOs;
using Airtraffic.Controllers.Response;

namespace Airtraffic.Mapping
{
    public class ModelToDTO : Profile
    {
        public ModelToDTO()
        {
            CreateMap<Airport, AirportDTO>();
            CreateMap<Aircraft, AircraftDTO>();
            CreateMap<Glider, GliderDTO>();
            CreateMap<Airplane, AirplaneDTO>();
            CreateMap<Airplane, CreateUpdateAirplaneDTO>();
            CreateMap<Glider, CreateUpdateGliderDTO>();
            CreateMap<FlightInformation, FlightDTO>();
        }
    }
}
