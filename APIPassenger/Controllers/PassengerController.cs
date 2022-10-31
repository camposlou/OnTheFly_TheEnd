using APIPassenger.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace APIPassenger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerServices _passengerService;
        private readonly AddressPassengerServices _addressServices;
        private readonly DeletedPassengerServices _deletedPassengerService;
        private readonly RestrictedServices _restrictService;
        public PassengerController(PassengerServices passangerServices, AddressPassengerServices addressServices, DeletedPassengerServices deletedPassengerService, RestrictedServices restrictService)
        {
            _passengerService = passangerServices;
            _addressServices = addressServices;
            _deletedPassengerService = deletedPassengerService;
            _restrictService = restrictService;
        }
        #region GET ALL
        [HttpGet]
        public ActionResult<List<Passenger>> Get() => _passengerService.Get();
        #endregion
        #region GET BY CPF
        [HttpGet("CPF/{cpf}", Name = "GetPassengerbyCPF")]
        public ActionResult<Passenger> Get(string cpf)
        {
            cpf = CPFFormatting(cpf);
            var passenger = _passengerService.Get(cpf);
            if (passenger == null)
                return NotFound("Passageiro não encontrado");
            return Ok(passenger);
        }
        #endregion
        #region CREATE
        [HttpPost]
        public ActionResult<Passenger> Create(Passenger passenger)
        {
            #region Inserção de endereço - OK
            var cep = passenger.Address.ZipCode;
            var address = _addressServices.GetAdress(cep).Result;
            if (address == null)
                return NotFound("Endereço não encontrado");
            else
                address.Number = passenger.Address.Number;
            address.Complement = passenger.Address.Complement;
            passenger.Address = address;
            #endregion
            if (CPFValidator(passenger.CPF))
            {
                #region Verificação de CPF valido - OK
                passenger.CPF = CPFFormatting(passenger.CPF);
                var pass = _passengerService.Get(passenger.CPF);
                if (pass == null)
                {
                    if (passenger.Gender == 'M' || passenger.Gender == 'F' || passenger.Gender == 'O')
                    {
                        passenger.DtRegister = System.DateTime.Now;
                        #region verfificação de cpf restrito e inserção na collection restricted
                        if (passenger.Status == false)
                        {
                            #region inserção na collection passenger - OK     
                            if (passenger.Phone != "")
                            {
                                passenger.Phone = PhoneFormmating(passenger.Phone);
                            }
                            _passengerService.Create(passenger);
                            return CreatedAtRoute("GetPassengerbyCPF", new { cpf = passenger.CPF.ToString() }, passenger);
                            #endregion
                        }
                        else
                        {
                            #region inserção na collection restricted e passenger - OK
                            Restricted restrictedpassenger = new Restricted();
                            if (passenger.Phone != "")
                            {
                                passenger.Phone = PhoneFormmating(passenger.Phone);
                            }
                            restrictedpassenger.CPF = passenger.CPF;
                            restrictedpassenger.Name = passenger.Name;
                            restrictedpassenger.Gender = passenger.Gender;
                            restrictedpassenger.Phone = passenger.Phone;
                            restrictedpassenger.DtBirth = passenger.DtBirth;
                            restrictedpassenger.DtRegister = passenger.DtRegister;
                            restrictedpassenger.Status = passenger.Status;
                            restrictedpassenger.Address = passenger.Address;
                            _restrictService.Create(restrictedpassenger);
                            _passengerService.Create(passenger);
                            return CreatedAtRoute("GetPassengerbyCPF", new { cpf = passenger.CPF.ToString() }, passenger);
                            #endregion
                        }
                        #endregion 
                    }
                    else
                        return BadRequest("Genero não pode ser diferente de: F(Feminino), M(Masculino) ou O (Outro)");
                }
                else
                    return BadRequest("CPF ja cadastrado!");
            }
            #endregion 
            else
                return BadRequest("CPF invalido!");
        }
        #endregion
        #region PUT
        [HttpPut]
        public ActionResult<Passenger> Put(Passenger passengerIn, string cpf)
        {
            passengerIn.CPF = CPFFormatting(passengerIn.CPF);
            cpf = CPFFormatting(cpf);
            var passenger = _passengerService.Get(cpf);
            if (passenger == null)
            {
                return NotFound("Passageiro não encontrado!");
            }
            if (cpf != passengerIn.CPF)
            {
                return BadRequest("Não é possivel alterar o cpf!");
            }
            if (passengerIn.DtBirth != passenger.DtBirth)
            {
                return BadRequest("Não é possivel alterar a data de nascimento!");
            }
            if (passengerIn.DtRegister != passenger.DtRegister)
            {
                return BadRequest("Não é possivel alterar a data de registro!");
            }
            if (passengerIn.Gender == 'M' || passengerIn.Gender == 'F' || passengerIn.Gender == 'O')
            {
                var cep = passengerIn.Address.ZipCode;
                var address = _addressServices.GetAdress(cep).Result;
                if (address == null)
                    return NotFound();
                address.Number = passengerIn.Address.Number;
                address.Complement = passengerIn.Address.Complement;
                passengerIn.Address = address;
                passengerIn.CPF = cpf;
                if (passengerIn.Phone != "")
                {
                    passengerIn.Phone = PhoneFormmating(passengerIn.Phone);
                }
                _passengerService.Put(cpf, passengerIn);
                return NoContent();
            }
            else
                return BadRequest("Genero não pode ser diferente de: F(Feminino), M(Masculino) ou O (Outro)");
        }
        #endregion
        #region DELETE
        [HttpDelete]
        public ActionResult<Passenger> Remove(string cpf)
        {
            cpf = CPFFormatting(cpf);
            var passenger = _passengerService.Get(cpf);
            if (passenger == null)
                return NotFound("Passageiro não encontrado!");
            DeletedPassenger deletedpassenger = new DeletedPassenger();
            deletedpassenger.CPF = passenger.CPF;
            deletedpassenger.Name = passenger.Name;
            deletedpassenger.Gender = passenger.Gender;
            deletedpassenger.Phone = passenger.Phone;
            deletedpassenger.DtBirth = passenger.DtRegister;
            deletedpassenger.Status = passenger.Status;
            deletedpassenger.Address = passenger.Address;
            _deletedPassengerService.Create(deletedpassenger);
            _passengerService.Remove(passenger);
            return NoContent();
        }
        #endregion
        #region VALIDATORS
        private static bool CPFValidator(string vrCPF)
        {
            string value = vrCPF.Replace(".", "");
            value = value.Replace("-", "");
            if (value.Length != 11)
                return false;
            bool equal = true;
            for (int i = 1; i < 11 && equal; i++)
                if (value[i] != value[0])
                    equal = false;
            if (equal || value == "12345678909")
                return false;
            int[] numbers = new int[11];
            for (int i = 0; i < 11; i++)
                numbers[i] = int.Parse(
                  value[i].ToString());
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];
            int result = sum % 11;
            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (11 - i) * numbers[i];
            result = sum % 11;
            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else
                if (numbers[10] != 11 - result)
                return false;
            return true;
        }
        public static string CPFFormatting(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
        public static string PhoneFormmating(string phone)
        {
            return Convert.ToUInt64(phone).ToString(@"(00)00000-0000");
        }
        #endregion
    }
}
