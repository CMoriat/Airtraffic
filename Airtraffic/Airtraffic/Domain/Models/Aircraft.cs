using Airtraffic.Helpers;

namespace Airtraffic.Domain.Models
{
    public abstract class Aircraft
    {
        public int Id { get; set; }
        public string AircraftType { get; set; }
        public string Name { get; set; }
        public double AirspeedInKilometersPerHour { get; set; }
        public int PilotCount { get; set; }
        public int AirportId { get; set; }
        public Airport CurrentAirport { get; set; }
        public virtual FlightInformation Fly(Airport destinationAirport) 
        {
            var flightInformation = new FlightInformation();
            flightInformation.DistanceInKilometers = FlightCalculator.CalculateDistance(CurrentAirport.Longitude, CurrentAirport.Latitude,
                                                            destinationAirport.Longitude, destinationAirport.Latitude);
            
            flightInformation.EstimatedTimeInHours = FlightCalculator.CalculateEstimatedTime(flightInformation.DistanceInKilometers, AirspeedInKilometersPerHour);

            flightInformation.FuelUseInKilometresPerLitre = 0;

            return flightInformation;
        }
    }
}
