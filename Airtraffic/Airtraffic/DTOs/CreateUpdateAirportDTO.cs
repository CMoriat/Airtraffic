using System.ComponentModel.DataAnnotations;

namespace Airtraffic.DTOs
{
    public class CreateUpdateAirportDTO
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }
    }
}
