using Airtraffic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airtraffic.Domain.Services.Communication
{
    public class AirportResponseObject : BaseResponse
    {
        public Airport Airport { get; protected set; }

        public AirportResponseObject(bool success, string message, Airport airport) : base(success, message)
        {
            Airport = airport;
        }


        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public AirportResponseObject(Airport airport) : this(true, string.Empty, airport)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AirportResponseObject(string message) : this(false, message, null)
        { }
    }
}
