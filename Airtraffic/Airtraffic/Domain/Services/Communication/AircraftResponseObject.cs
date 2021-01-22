using Airtraffic.Domain.Models;
using Airtraffic.Domain.Services.Communication;

namespace Airtraffic.Domain.Services
{
    public class AircraftResponseObject<T> : BaseResponse where T : Aircraft
    {
        public T Aircraft { get; protected set; }

        public AircraftResponseObject(bool success, string message, T aircraft) : base(success, message)
        {
            Aircraft = aircraft;
        }


        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public AircraftResponseObject(T aircraft) : this(true, string.Empty, aircraft)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AircraftResponseObject(string message) : this(false, message, null)
        { }
    }
}