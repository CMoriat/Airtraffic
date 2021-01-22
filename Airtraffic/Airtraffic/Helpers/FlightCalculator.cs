using System;

namespace Airtraffic.Helpers
{
    public static class FlightCalculator
    {
        public static double CalculateDistance(double startLongitude, double startLatitude, double endLongitude, double endLatitude)
        {
            var d1 = startLatitude * (Math.PI / 180.0);
            var num1 = startLongitude * (Math.PI / 180.0);
            var d2 = endLatitude * (Math.PI / 180.0);
            var num2 = endLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 63765.000 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public static double CalculateEstimatedTime(double distance, double airspeed)
        {
            return distance / airspeed;
        }

        public static double CalculateFuelUse(double distance, double fuelconsumption)
        {
            return distance / fuelconsumption;
        }
    }
}
