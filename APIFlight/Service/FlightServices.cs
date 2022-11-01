using APIFlight.Utils;
using Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIFlight.Services
{
    public class FlightServices
    {
        private readonly IMongoCollection<Flight> _flights;

        #region Method
        public FlightServices(IDatabaseSettings settings)
        {
            var flight = new MongoClient(settings.ConnectionString);
            var database = flight.GetDatabase(settings.DatabaseName);
            _flights = database.GetCollection<Flight>(settings.FlightCollectionName);
        }
        #endregion

        #region POST Flight        
        public async Task<Flight> CreateAsync(Flight flight)
        {
            await _flights.InsertOneAsync(flight);
            return flight;
        }
        #endregion

        #region Get List Flight
        public  List<Flight> GetListFlight() =>  _flights.Find(flight => true).ToList();
        #endregion

        #region Get One Flight Id
        public Flight GetOne(string id) => _flights.Find(flight => flight.Id == id).FirstOrDefault();
        #endregion

        #region Get One Flight Departure
        public Flight GetOneFlight(DateTime departure, string rab)
        {
            var flightRABList = _flights.Find(flight => flight.Plane.RAB.ToUpper() == rab.ToUpper()).ToList();
            foreach (var flight in flightRABList)
            {
                if (flight.Departure.ToString("dd/MM/yyyy") == departure.ToString("dd/MM/yyyy")) return flight;
            }
            return null;
        }
            
        #endregion

        #region Get API 
        public async Task<Aircraft> GetByAircraft(string rab)
        {            
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync("https://localhost:44321/api/Aircraft/" + rab);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            var aircraft = JsonSerializer.Deserialize<Aircraft>(JsonString);

            return aircraft;
            
        }
        public async Task<Airport> GetIata(string iata)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync("https://localhost:44315/Airport/" + iata);
            string JsonString = await airportresponse.Content.ReadAsStringAsync();
            var airport = JsonSerializer.Deserialize<Airport>(JsonString);
            return airport;

        }
        //public async Task<Blocked> Get(string cnpj)
        //{
        //    cnpj = cnpj.Trim();
        //    cnpj = cnpj.Replace("/", "%2F");
        //    Blocked blocked;

        //    using (HttpClient _blockedClient = new HttpClient())
        //    {

        //        HttpResponseMessage response = await _blockedClient.GetAsync("https://localhost:44314/api/Blocked/" + cnpj);
        //        var blockedJson = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //            return blocked = JsonSerializer.Deserialize<Blocked>(blockedJson);
        //        else
        //            return null;

        //    }
        //}       
        public async Task<Aircraft> UpdateAircraft(string rab, DateTime dtFlight)
        {
            Aircraft aircraft;
            using (HttpClient _aircraftClient = new HttpClient())
            {

                HttpResponseMessage response = await _aircraftClient.PutAsync($"https://localhost:44321/api/Aircraft/{rab},{dtFlight}", null);
                var aircraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return aircraft = JsonSerializer.Deserialize<Aircraft>(aircraftJson);
                else
                    return null;

            }
        }
        #endregion

        #region PUT Flight Id
        public void Update(string id, Flight flightIn) =>
        
           _flights.ReplaceOne(flight => flight.Id == id, flightIn);        
        #endregion

        #region Put Flight Cancel
        public void UpdateCancel (string id, Flight flightIn) => 
            _flights.ReplaceOne(flight => flight.Id == id, flightIn);
        #endregion


    }
}
