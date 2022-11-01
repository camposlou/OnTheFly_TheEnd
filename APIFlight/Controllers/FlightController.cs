using APIFlight.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFlight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightServices _flightService;
       


        #region Method
        public FlightController(FlightServices flightservice)
        {
            _flightService = flightservice;
           

        }
        #endregion

        #region Get List Flight
        [HttpGet]
        public ActionResult<List<Flight>> Get() => _flightService.GetListFlight();
        #endregion

        #region Get One Flight Departure
        [HttpGet("GetOneFlight/{departure}/{rab}")]
        public ActionResult<Flight> Get(DateTime departure, string rab)
        {
            var flight = _flightService.GetOneFlight(departure, rab);
            if (flight == null || flight.Status == false)
                return NotFound("Voo não encontrado!");

            return Ok(flight);
        }
        #endregion

        #region Get One Flight Id
        [HttpGet("{id}", Name = "GetFlight")]
        public ActionResult<Flight> GetOne(string id)
        {
            var flight = _flightService.GetOne(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
        #endregion

        #region Post Flight
        [HttpPost]
        public async Task<ActionResult<Flight>> CreateAsync(string iata, DateTime departure, string rab)
        {
            var destiny = await _flightService.GetIata(iata);
            if(destiny == null)
                return NotFound("Aeroporto não encontrado!");

            var plane = await _flightService.GetByAircraft(rab);
            if (plane == null) 
                return NotFound("Aeronave não encontrada!");

            if (departure < DateTime.Now)
                return NotFound("Impossivel criar voo com data retroativa!");

            if (plane.Company.Status == false)
                return BadRequest("Não pode ser cadastrado voos para essa companhia!");

            //if (_flightService.GetOneFlight(flight.Departure, plane.RAB) == null)
            //    return BadRequest("Aeronave já possui voo nesse dia!");
           

            var flight = new Flight()
            {
                Departure = departure,
                Destiny = destiny,
                Plane = plane,
                Sale = 0,
                Status = true
            };
            await _flightService.CreateAsync(flight);
           // await _flightService.UpdateAircraft(plane.RAB, departure);

            return CreatedAtRoute("GetFlight", new { id = flight.Id }, flight);
        }
        #endregion

        #region Put Flight
        [HttpPut("flightcancel/{id}")]
        public ActionResult<Flight> UpdateCancel(string id)
        {
            var flight = _flightService.GetOne(id);

            if (flight == null || flight.Status == false)
            {
                return NotFound();
            }           

            flight.Status = false;

            _flightService.UpdateCancel(id, flight);

            return CreatedAtRoute("GetFlight", new { id = flight.Id }, flight);
        }
        #endregion

        #region Put Flight Id
        [HttpPut("{id}")]
        public ActionResult<Flight> Update(string id, Flight flightIn)
        {
            var flight = _flightService.GetOne(id);

            if (flight == null || flight.Status == false)
            {
                return NotFound();
            }

            _flightService.Update(id, flightIn);

            return CreatedAtRoute("GetFlight", new { id = flight.Id }, flightIn); 
        }
        #endregion

    }
}
