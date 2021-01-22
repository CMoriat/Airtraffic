using Airtraffic.Controllers.Response;
using Airtraffic.Domain.Models;
using Airtraffic.DTOs;
using AutoMapper;

namespace Airtraffic.Mapping
{
    public class DTOToModel : Profile
    {
        public DTOToModel()
        {
            CreateMap<CreateUpdateAirportDTO, Airport>();
            CreateMap<AirportDTO, Airport>();

            CreateMap<CreateUpdateAircraftDTO, Aircraft>();
            CreateMap<AircraftDTO, Aircraft>();

            CreateMap<GliderDTO, Glider>();
            CreateMap<AirplaneDTO, Airplane>();

            CreateMap<CreateUpdateAirplaneDTO, Airplane>();
            CreateMap<CreateUpdateGliderDTO, Glider>();
        }
    }
}
