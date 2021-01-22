using Airtraffic.Domain.Models;
using Airtraffic.Domain.Services;
using Airtraffic.DTOs;
using Airtraffic.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airtraffic.Controllers
{
    [Route("/api/[controller]")]
    public class AirportsController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public AirportsController(IAirportService airportService, IMapper mapper)
        {
            _airportService = airportService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<AirportDTO>> GetAllAsync()
        {
            var airports = await _airportService.ListAsync();
            var dtos = _mapper.Map<IEnumerable<Airport>, IEnumerable<AirportDTO>>(airports);
            return dtos;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUpdateAirportDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var airport = _mapper.Map<CreateUpdateAirportDTO, Airport>(dto);
            var result = await _airportService.CreateAsync(airport);

            if (!result.Success) return BadRequest(result.Message);

            var airportDTO = _mapper.Map<Airport, AirportDTO>(result.Airport);
            return Ok(airportDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CreateUpdateAirportDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var airport = _mapper.Map<CreateUpdateAirportDTO, Airport>(dto);
            var result = await _airportService.UpdateAsync(id, airport);

            if (!result.Success)
                return BadRequest(result.Message);

            var airportDTO = _mapper.Map<Airport, AirportDTO>(result.Airport);
            return Ok(airportDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _airportService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var airportDTO = _mapper.Map<Airport, AirportDTO>(result.Airport);
            return Ok(airportDTO);
        }
    }
}
