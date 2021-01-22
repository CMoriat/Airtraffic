using Airtraffic.Domain.Models;

namespace Airtraffic.Domain.Services.Communication
{
    public class FlightResponse : BaseResponse
    {
        public FlightInformation FlightInformation { get; protected set; }

        public FlightResponse(bool success, string message, FlightInformation flightInformation) : base(success, message)
        {
            FlightInformation = flightInformation;
        }


        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public FlightResponse(FlightInformation flightInformation) : this(true, string.Empty, flightInformation)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public FlightResponse(string message) : this(false, message, null)
        { }
    }
}
