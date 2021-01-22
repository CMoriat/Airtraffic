using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airtraffic.Domain.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public IList<Aircraft> Aircrafts { get; set; } = new List<Aircraft>();

    }
}
