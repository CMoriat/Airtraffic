using Airtraffic.DTOs;

namespace Airtraffic.Controllers.Response
{
    public class AircraftDTO
    {
        public int Id { get; set; }
        public string AircraftType { get; set; }
        public string Name { get; set; }
        public double AirspeedInKilometersPerHour { get; set; }
        public int PilotCount { get; set; }
        public int AirportId { get; set; }
        public AirportDTO CurrentAirport { get; set; }
    }

    public class GliderDTO : AircraftDTO { }
    public class AirplaneDTO : AircraftDTO {
        public double FuelConsumptionInKilometersPerLitre { get; set; }
    }
}
