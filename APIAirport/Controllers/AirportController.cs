using Domain.Models;
using APIAirport.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace APIAirport.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportServices _airportServices;
        public AirportController(AirportServices airportServices)
        {
            _airportServices = airportServices;
        }

        //[HttpPost]
        //public async Task<ActionResult<Airport>> Create(Airport airport)
        //{
        //   await _airportServices.Create(airport);
        //    return Ok();
        //}
        
        //[HttpGet]
        //public async Task<ActionResult<List<Airport>>> Get() => await _airportServices.Get();

        [HttpGet("{iata}", Name = "GetAirportIata")]
        public ActionResult<Airport> Get(string iata)
        {
            var airport =  _airportServices.GetIata(iata);

            if (airport == null)
                return NotFound();

            return airport;
        }
       

        [HttpGet("/ByCity/{city_code}", Name = "GetAirportCity")]
        public ActionResult<List<Airport>> GetByCity(string city_code)
        {
            var airport = _airportServices.GetByCity(city_code);

            if (airport == null)
                return NotFound();

            return airport;
        }

        [HttpGet("/ByIcao/{icao}", Name = "GetAirportIcao")]
        public ActionResult<List<Airport>> GetByIcao(string icao)
        {
            var airport = _airportServices.GetByIcao(icao);

            if (airport == null)
                return NotFound();

            return airport;
        }
    }
}
