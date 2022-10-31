using APIFlight.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


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

        //#region Get List Flight
        //[HttpGet]
        //public ActionResult<List<Flight>> Get() => _flightService.GetListFlight();
        //#endregion

        //#region Get One Flight Departure
        //[HttpGet("{departure}", Name = "GetFlightDeparture")]
        //public ActionResult<Flight> Get(DateTime departure)
        //{
        //    var flight = _flightService.GetOneFlight(departure);
        //    if (flight == null)
        //        return NotFound("Voo não encontrado!");

        //    return Ok(flight);
        //}
        //#endregion


        //    #region Post Flight
        //    [HttpPost]
        //    public ActionResult<Flight> Create(Flight flight, string iata, DateTime departure, string rab, double hours, double minutes, string cnpj)
        //    {
        //        departure = departure.AddHours(hours).AddMinutes(minutes);
        //        iata = iata.ToUpper();
        //        if (departure < DateTime.Now)
        //        {
        //            return NotFound("Impossivel criar voo com data retroativa!");
        //        }
        //        else
        //        {
        //            var destiny = _flightService.GetIata(iata);
        //            if (destiny == null)
        //            {
        //                return NotFound("Destino nao encontrado!");
        //            }
        //            else
        //        {
        //            var plane = _flightService.GetByAircraft(rab);
        //            if (plane == null)
        //            {
        //                return NotFound("Aeronave não cadastrada!");
        //            }
        //            else
        //            {
        //                var blocked = _flightService.Get(cnpj);
        //                if (blocked.Status = true)
        //                {
        //                    return NotFound("Não foi possível cadastrar voo. Companhia bloqueada!");
        //                }
        //                else
        //                {
        //                    Flight flight = new Flight() { Sale = plane.Capacity, Status = true, Plane = plane, Destiny = destiny, Departure = departure };
        //                    _flightService.Create(flight);
        //                        _flightService.UpdateAircraft(flight.Aircraft);
        //                    return CreatedAtRoute("GetFlight", new { destiny = flight.Destiny.ToString() }, flight);
        //                }
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region Put Flight
        [HttpPut]
        public ActionResult<Flight> Update(DateTime departure, bool newStatus, Flight flightIn)
        {
            var flight = _flightService.GetOneFlight(departure);

            if (flightIn == null || flightIn.Status == false)
            {
                return NotFound();
            }
            if (flightIn.Departure != flightIn.Departure)
            {
                return BadRequest("Não é possivel alterar a data de voo!");
            }

            flightIn.Status = newStatus;

            _flightService.Update(departure, flightIn);

            return NoContent();
        }
        #endregion

    }
}
