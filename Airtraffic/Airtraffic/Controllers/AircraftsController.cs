using Airtraffic.Controllers.Response;
using Airtraffic.Domain.Models;
using Airtraffic.Domain.Services;
using Airtraffic.Domain.Services.Communication;
using Airtraffic.DTOs;
using Airtraffic.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airtraffic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController<T, TResponse> : ControllerBase where T : Aircraft where TResponse : AircraftDTO
    {
        private readonly IAircraftService<T> _aircraftService;
        private readonly IMapper _mapper;

        public AircraftsController(IAircraftService<T> aircraftService, IMapper mapper)
        {
            _aircraftService = aircraftService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IList<TResponse>> Get()
        {
            var aircrafts = await _aircraftService.ListAllAircrafts();
            var aircraftResponse = _mapper.Map<IList<T>, IList<TResponse>>(aircrafts);
            return aircraftResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TResponse>> Get(int id)
        {
            var aircraft = await _aircraftService.FindById(id);
            var aircraftResponse  = _mapper.Map<T, TResponse>(aircraft);
            return aircraftResponse;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TResponse dto)
        {
            var aircraft = _mapper.Map<TResponse, T>(dto);
            var result = await _aircraftService.UpdateAsync(id, aircraft);

            if (!result.Success)
                return BadRequest(result.Message);

            var aircraftDTO = _mapper.Map<T, TResponse>(result.Aircraft);
            return Ok(aircraftDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _aircraftService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var aircraftDTO = _mapper.Map<T, TResponse>(result.Aircraft);
            return Ok(aircraftDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TResponse>> Post([FromBody]TResponse dto)
        {
            var aircraft = _mapper.Map<TResponse, T>(dto);
            var result = await _aircraftService.CreateAsync(aircraft);

            if (!result.Success)
                return BadRequest(result.Message);

            var aircraftDTO = _mapper.Map<T, TResponse>(result.Aircraft);
            return Ok(aircraftDTO);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<ActionResult<FlightResponse>> Fly(int id, [FromBody] FlightRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _aircraftService.Fly(id, dto.DestinationAirportId);

            if (!result.Success)
                return BadRequest(result.Message);

            var flightDTO = _mapper.Map<FlightInformation, FlightDTO>(result.FlightInformation);
            return Ok(flightDTO);
        }
    }

    [Route("api/Airplane")]
    [ApiController]
    public class AirPlaneController : AircraftsController<Airplane, AirplaneDTO>
    {
        public AirPlaneController(IAircraftService<Airplane> aircraftService, IMapper mapper) : base(aircraftService, mapper)
        { }
    }

    [Route("api/Glider")]
    [ApiController]
    public class GliderController : AircraftsController<Glider, GliderDTO>
    {
        public GliderController(IAircraftService<Glider> aircraftService, IMapper mapper) : base(aircraftService, mapper)
        { }
    }
}
