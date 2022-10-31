using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using APISale.Services;

namespace APISale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleServices _saleService;
        public SaleController(SaleServices settings)
        {
            _saleService = settings;
        }
        [HttpGet]
        public ActionResult<List<Sale>> GetAll()
        {
            var sales = _saleService.GetAll();
            if (sales == null)
            {
                return NotFound("Nao existem vendas cadastradas!");
            }
            return Ok(sales);
        }
        [HttpGet("{cpf}", Name = "GetCpf")]
        public ActionResult<Sale> GetCpf(string cpf)
        {
            var sale = _saleService.GetSale(cpf);
            if (sale == null)
            {
                return NotFound("Nao existem vendas cadastradas com esse cpf!");
            }
            return Ok(sale);
        }
        [HttpPost]
        public ActionResult<Sale> Post(string cpfs, DateTime dateflight, bool sold, bool reserverd)
        {
            string[] listcpf = cpfs.Split(',');
            var passagensAtribute = new List<Passenger>();
            for (int i = 0; i < listcpf.Length; i++)
            {
                string cpfperson = listcpf[i];
                cpfperson = cpfperson.Substring(0, 3) + "." + cpfperson.Substring(3, 3) + "." + cpfperson.Substring(6, 3) + "-" + cpfperson.Substring(9, 2);
                var passenger = _saleService.GetPassenger(cpfperson);
                if (passenger == null)
                {
                    return BadRequest($"Não encotramos esse Cpf{listcpf[i]} em nossos Cadastros de Passageiro!");
                }
                int age = DateTime.Now.Year - passenger.DtBirth.Year;
                if (i == 0 && age < 18)
                    return BadRequest("É necessario ter mais de 18 Anos para comprar a Passagem!");
                else
                {
                    passagensAtribute.Add(passenger);
                }
            }
            var flight = _saleService.GetFlight(dateflight); //falar com louise, fazer metodo de achar voo por data, iata, horas e minutos
            if (flight == null)
            {
                return BadRequest("Voo não localizado!");
            }
            else
            {
                if (sold == false && reserverd == true)
                {
                    if ((flight.Sale - listcpf.Length) < 0)
                    {
                        return BadRequest("Quantidades de vendas de voo excedida, venda não pode ser realizada");
                    }
                    flight.Sale = flight.Sale - listcpf.Length;
                    _ = _saleService.PutFlight(flight.Departure, flight.Status, flight);
                    Sale sales = new() { Passenger = passagensAtribute };
                    sales.Flight = flight;
                    sales.Reserved = true;
                    sales.Sold = false;
                    return Ok(_saleService.Create(sales));
                }
                else if (sold == true && reserverd == false)
                {
                    if ((flight.Sale - listcpf.Length) < 0)
                    {
                        return BadRequest("Quantidades de vendas de voo excedida, venda não pode ser realizada");
                    }
                    flight.Sale = flight.Sale - listcpf.Length;
                    _ = _saleService.PutFlight(flight.Departure, flight.Status, flight);
                    Sale sales = new() { Passenger = passagensAtribute };
                    sales.Flight = flight;
                    sales.Reserved = false;
                    sales.Sold = true;
                    return Ok(_saleService.Create(sales));
                }
                else
                {
                    return BadRequest("Inconsistencia de dados de venda!");
                }
            }
        }
        [HttpPut]
        public ActionResult<Sale> Put(string cpf, bool reserva)
        {
            var sale = _saleService.GetSale(cpf);
            if (sale == null)
                return BadRequest("Não existe venda efetuada para passageiro registrados com esse CPF");
            sale.Reserved = reserva;
            _saleService.Put(cpf, sale);
            return Ok(sale);
        }
    }
}
