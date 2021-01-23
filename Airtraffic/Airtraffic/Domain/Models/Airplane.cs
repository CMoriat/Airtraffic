using Airtraffic.Helpers;

namespace Airtraffic.Domain.Models
{
    public class Airplane : Aircraft
    {
        public double FuelConsumptionInKilometerPerLitre { get; set; }
        public override FlightInformation Fly(Airport destinationAirport)
        {
            var flightInformation = new FlightInformation();
            flightInformation.DistanceInKilometers = FlightCalculator.CalculateDistance(CurrentAirport.Longitude, CurrentAirport.Latitude,
                                                            destinationAirport.Longitude, destinationAirport.Latitude);

            flightInformation.EstimatedTimeInHours = FlightCalculator.CalculateEstimatedTime(flightInformation.DistanceInKilometers, AirspeedInKilometersPerHour);

            flightInformation.FuelUseInLitres = FlightCalculator.CalculateFuelUse(flightInformation.DistanceInKilometers, FuelConsumptionInKilometerPerLitre);

            return flightInformation;
        }
    }
}
