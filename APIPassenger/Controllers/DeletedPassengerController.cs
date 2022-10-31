using APIPassenger.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APIPassenger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeletedPassengerController : ControllerBase
    {
        private readonly DeletedPassengerServices _deletedPassengerService;
        public DeletedPassengerController(DeletedPassengerServices deletedPassengerService)
        {
            _deletedPassengerService = deletedPassengerService;
        }

        #region GET ALL
        [HttpGet]
        public ActionResult<List<DeletedPassenger>> Get() => _deletedPassengerService.Get();
        #endregion

        #region GET BY CPF
        [HttpGet("CPF/{cpf}", Name = "GetDeletedPassengerbyCPF")]
        public ActionResult<DeletedPassenger> Get(string cpf)
        {
            cpf = FormatCPF(cpf);
            var passenger = _deletedPassengerService.Get(cpf);
            if (passenger == null)
                return NotFound();
            return Ok(passenger);
        }
        #endregion

        #region VALIDATORS
        public static string FormatCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
        #endregion
    }
}
