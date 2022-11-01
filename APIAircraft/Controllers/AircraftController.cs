using System;
using System.Collections.Generic;
using APIAircraft.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIAircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        #region Attribute
        private readonly AircraftServices _aircraftService;
        private readonly DeletedAircraftServices _deletedAircraftService;
        #endregion

        #region Method
        public AircraftController(AircraftServices aircraftService, DeletedAircraftServices deletedAirCraftService)
        {
            _aircraftService = aircraftService;
            _deletedAircraftService = deletedAirCraftService;
        }
        #endregion

        #region Read All AirCraft - OK
        [HttpGet]
        public ActionResult<List<Aircraft>> GetAllAircraft()
        {
            var aircraft = _aircraftService.GetAllAircraft();
            if (aircraft == null) return BadRequest("Não foi encontrado nenhuma aeronave cadastrada!");
            return Ok(aircraft);
        }
        #endregion

        #region Read AirCraft One From RAB - OK
        [HttpGet("{rab:length(6)}", Name = "GetAirCraft")]
        public ActionResult<Aircraft> GetByAircraft(string rab)
        {

            if (rab == null) return NotFound("RAB não foi encontrado!\nTente novamente!");
            else if (rab.Length == 5 || rab.Length == 6)
            {
                rab = rab.ToLower().Trim();
                rab = rab.Replace("-", "");
                rab = rab.Substring(0, 2) + "-" + rab.Substring(2, 3);
            }
            else
            {
                return NotFound("RAB inválido!\nTente novamente!");
            }

            var aircraft = _aircraftService.GetByAircraft(rab);
            if (aircraft == null)
                return NotFound("Esse RAB não foi encontrado!\nInforme um RAB válido.");

            return Ok(aircraft);
        }
        #endregion

        #region Create AirCraft - OK FALTA VALIDACAO PREFIXO
        [HttpPost("{cnpj}")]
        public ActionResult<Aircraft> CreateAircraft(AircraftDTO aircraftIn, string cnpj)
        {
            //cnpj = aircraft.Company.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var company = _aircraftService.GetApiCompany(cnpj).Result;
            if (company == null) return NotFound("Companhia não cadastrada!");

            var rab = aircraftIn.RAB.Trim().ToLower();
            //var rabValidation = rab.Substring(0, 2);
            //if (rabValidation != "PT" && rabValidation != "PP" && rabValidation != "PR" && rabValidation != "PS")
            //    return BadRequest("Prefixo da aeronave incorreto!\nTente Novamente!");

            if (rab.Length < 5)
                return BadRequest("RAB inválido!\nTente novamente!");
            Aircraft aircraft = new Aircraft();
            aircraft.RAB = rab.Substring(0, 2) + "-" + rab.Substring(2, 3);
            aircraft.DtRegistry = DateTime.Now;
            aircraft.DtLastFlight = DateTime.Now;
            aircraft.Company = company;
            aircraft.Capacity = aircraftIn.Capacity;            
            if (aircraftIn.Capacity == 0)
                return BadRequest("Aeronave precisa ter um número de assentos superior a 0.");

            var dtLastFlight = aircraft.DtLastFlight;
            var dtRegistry = aircraft.DtRegistry;

            if (dtLastFlight < dtRegistry)
                return BadRequest("A data do último voo não pode ser menor que a data do registro da aeronave!");
            if (dtLastFlight > DateTime.Now)
                return BadRequest("A data do último voo não pode ser uma data futura!");

            _aircraftService.CreateAircraft(aircraft);
            return CreatedAtRoute("GetAirCraft", new { rab = aircraft.RAB.ToString() }, aircraft);
        }
        #endregion
        #region AirCraft Update - OK
        [HttpPut("{rab:length(6)},{newCapacity}")]
        public ActionResult<Aircraft> UpdateAircraft(string rab, Aircraft aircraftIn, int newCapacity, string cnpj, DateTime dtlastflight)
        {
            //Aircraft aircrafIn = new Aircraft()
            //{
            //    RAB = rab,
            //    DtRegistry = System.DateTime.Now,
            //    DtLastFlight = dtlastFlight,
            //};

            //Company company = _aircraftService.GetApiCompany(cnpj);

            //aircraftIn.Company = company;
            var aircraft = _aircraftService.GetByAircraft(rab);

            if (aircraft == null)
                return NotFound("Aeronave não encontrada!");

            if (rab != aircraftIn.RAB)
                return BadRequest("Não é possivel alterar o RAB!");

            if (aircraftIn.DtRegistry != aircraft.DtRegistry)
                return BadRequest("Não é possivel alterar a data do registro!");

            if (aircraftIn.DtLastFlight != aircraft.DtLastFlight)
                return BadRequest("Não é possivel alterar a data do último voo!");

            aircraftIn.Capacity = newCapacity;

            _aircraftService.UpdateAircraft(rab, aircraftIn);

            return NoContent();
        }
        #endregion

        #region Update DateFlight
        [HttpPut("{rab:length(6)},{dtFlight}")]
        public ActionResult<Aircraft> UpdateDateFlight(string rab, DateTime dtFlight)
        {

            var aircraft = _aircraftService.GetByAircraft(rab);
            if (aircraft == null)
                return NotFound("Aeronave não encontrada!");

            aircraft.DtLastFlight = dtFlight;

            _aircraftService.UpdateAircraft(rab, aircraft);

            return NoContent();
        }
        #endregion

        #region Delete AirCraft - OK
        [HttpDelete("{rab:length(6)}")]
        public ActionResult RemoveAircraft(string rab)
        {
            var aircraft = _aircraftService.GetByAircraft(rab);

            if (aircraft == null)
                return NotFound();

            DeletedAircraft deleteAircraft = new DeletedAircraft();
            deleteAircraft.RAB = aircraft.RAB;
            deleteAircraft.Capacity = aircraft.Capacity;
            deleteAircraft.DtRegistry = aircraft.DtRegistry;
            deleteAircraft.DtLastFlight = aircraft.DtLastFlight;

            _deletedAircraftService.Create(deleteAircraft);
            _aircraftService.RemoveAircraft(aircraft);
            return NoContent();
        }
        #endregion


    }
}
