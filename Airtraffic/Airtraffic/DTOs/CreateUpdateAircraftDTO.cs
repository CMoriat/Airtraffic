using System.ComponentModel.DataAnnotations;

namespace Airtraffic.DTOs
{
    public class CreateUpdateAircraftDTO
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public int PilotCount { get; set; }
        [Required]
        public double AirspeedInKilometersPerHour { get; set; }
        [Required]
        public int AirportId { get; set; }
    }

    public class CreateUpdateGliderDTO : CreateUpdateAircraftDTO { }
    public class CreateUpdateAirplaneDTO : CreateUpdateAircraftDTO
    {
        public double FuelConsumptionInLitresPerKilometer { get; set; }
    }
}