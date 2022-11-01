using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using APISale.Services;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Sale>> PostAsync(string cpfs, DateTime dateflight, bool sold, bool reserverd, string rab)
        {
            string[] listcpf = cpfs.Split(',');
            var passagensAtribute = new List<Passenger>();
            for (int i = 0; i < listcpf.Length; i++)
            {
                string cpfperson = listcpf[i];
                cpfperson = cpfperson.Substring(0, 3) + "." + cpfperson.Substring(3, 3) + "." + cpfperson.Substring(6, 3) + "-" + cpfperson.Substring(9, 2);
                Passenger passenger = await _saleService.GetPassenger(listcpf[i]);
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
            var flight = await _saleService.GetFlight(dateflight, rab);
            if (flight == null)
            {
                return BadRequest("Voo não localizado!");
            }
            else
            {
                if (sold == false && reserverd == true)
                {
                    if ((flight.Sale + listcpf.Length) > flight.Plane.Capacity)
                    {
                        return BadRequest("Quantidade de vendas excedidas!");
                    }
                    flight.Sale = flight.Sale + listcpf.Length;
                    _ = _saleService.PutFlight(flight.Id, flight.Sale);
                    Sale sales = new() { Passenger = passagensAtribute };
                    sales.Flight = flight;
                    sales.Reserved = true;
                    sales.Sold = false;
                    return Ok(_saleService.Create(sales));
                }
                else if (sold == true && reserverd == false)
                {
                    if ((flight.Sale + listcpf.Length) > flight.Plane.Capacity)
                    {
                        return BadRequest("Quantidade de vendas excedidas!");
                    }
                    flight.Sale = flight.Sale + listcpf.Length;
                    _ = _saleService.PutFlight(flight.Id, flight.Sale);
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

