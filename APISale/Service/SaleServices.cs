using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Driver;
using APISale.Utils;
using System.Web.Script.Serialization;
using System.Text.Json;

namespace APISale.Services
{
    public class SaleServices
    {
        private readonly IMongoCollection<Sale> _sale;
        public SaleServices(IDatabaseSettings settings)
        {
            var sale = new MongoClient(settings.ConnectionString);
            var database = sale.GetDatabase(settings.DatabaseName);
            _sale = database.GetCollection<Sale>(settings.SaleCollectionName);
        }
        public Sale Create(Sale sale)
        {
            _sale.InsertOne(sale);
            return sale;
        }
        public List<Sale> GetAll() => _sale.Find<Sale>(sale => true).ToList();
        public Sale GetSale(string cpf) => _sale.Find<Sale>(sales => sales.Passenger.Any(passager => passager.CPF == cpf)).FirstOrDefault();
        public void Put(string Cpf, Sale saleIn)
        {
            _sale.ReplaceOne(sale => sale.Passenger.Any(passager => passager.CPF == Cpf), saleIn);
            GetSale(Cpf);
        }
        //public Passenger GetPassenger(string cpf)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:44369/api/Passenger/CPF/{cpf}"); //url
        //    request.AllowAutoRedirect = false;
        //    HttpWebResponse verificaServidor = (HttpWebResponse)request.GetResponse();
        //    Stream stream = verificaServidor.GetResponseStream();
        //    if (stream == null) return null;
        //    StreamReader answerReader = new StreamReader(stream);
        //    string message = answerReader.ReadToEnd();
        //    return new JavaScriptSerializer().Deserialize<Passenger>(message);
        //}
        public async Task<Passenger> GetPassenger(string cpf)
        {
            var httpclient = new HttpClient();
            var saleresponse = await httpclient.GetAsync($"https://localhost:44369/api/Passenger/CPF/{cpf}");
            var JsonString = await saleresponse.Content.ReadAsStringAsync();
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            var sale = JsonSerializer.Deserialize<Passenger>(JsonString);

            return sale;

        }
        public async Task<Restricted> GetRestrictedPassenger(string cpf) //verificar passageiros restritos
        {
            Restricted restricted;
            using (HttpClient _restrictedClient = new HttpClient())
            {
                HttpResponseMessage response = await _restrictedClient.GetAsync($"https://localhost:44369/api/Restricted/CPF/{cpf}");
                var restrictedPassangerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return restricted = JsonSerializer.Deserialize<Restricted>(restrictedPassangerJson);
                else
                    return null;
            }
        }
        public async Task<Flight> GetFlight(DateTime date, string rab)
        {
            string datein = date.ToString();
            datein = datein.Trim();
            datein = datein.Replace("/", "-");
            string dateYear = datein.Substring(6, 4);
            string dateMounth = datein.Substring(3, 2);
            string dateDay = datein.Substring(0, 2);
            string dateFinal = dateYear + "-" + dateMounth + "-" + dateDay;
            var httpclient = new HttpClient();
            var saleresponse = await httpclient.GetAsync($"https://localhost:44385/api/Flight/GetOneFlight/{dateFinal}/{rab}");
            var JsonString = await saleresponse.Content.ReadAsStringAsync();
            var sales = JsonSerializer.Deserialize<Flight>(JsonString);
            return sales;
        }
        public async Task<Flight> PutFlight(string id, int sale) //verificar voos
        {
            Flight flightReturn;
            using (HttpClient _flightClient = new HttpClient())
            {
                HttpResponseMessage response = await _flightClient.PutAsync($"https://localhost:44385/api/Flight/{id}/{sale}", null);
                var flightJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return flightReturn = JsonSerializer.Deserialize<Flight>(flightJson);
                else
                    return null;
            }
        }
    }
}
