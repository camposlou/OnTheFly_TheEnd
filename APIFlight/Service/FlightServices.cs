using APIFlight.Utils;
using Domain.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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
        
        public  Flight Create(Flight flight)
        {
            _flights.InsertOne(flight);
            return flight;
        }
        #endregion

        #region Get List Flight
        public  List<Flight> GetListFlight() =>  _flights.Find(flight => true).ToList();
        #endregion

        #region Get One Flight Departure
        public Flight GetOneFlight(DateTime departure) => _flights.Find    //Rever
            (flight => flight.Departure == departure).FirstOrDefault();
        #endregion

        #region Get API 
        public async Task<Aircraft> GetByAircraft(string rab)
        {
            Aircraft aircraft;

            using (HttpClient _aircraftClient = new HttpClient())
            {
                             
                HttpResponseMessage response = await _aircraftClient.GetAsync("https://localhost:44321/api" + rab );
                var aircraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return aircraft = JsonConvert.DeserializeObject<Aircraft>(aircraftJson);
                else
                    return null;

            }
        }

        public async Task<Airport> GetIata(string iata)
        {
           Airport airport;

            using (HttpClient _airportClient = new HttpClient())
            {

                HttpResponseMessage response = await _airportClient.GetAsync("http://localhost:11306/api" + iata);
                var airportJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return airport = JsonConvert.DeserializeObject<Airport>(airportJson);
                else
                    return null;

            }
        }

        public async Task<Blocked> Get(string cnpj)
        {
            Blocked blocked;

            using (HttpClient _blockedClient = new HttpClient())
            {

                HttpResponseMessage response = await _blockedClient.GetAsync("http://localhost:10676/api" + cnpj);
                var blockedJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return blocked = JsonConvert.DeserializeObject<Blocked>(blockedJson);
                else
                    return null;

            }
        }
        #endregion

        #region PUT Flight
        public void Update(DateTime departure, Flight flightIn)
        {
           _flights.ReplaceOne(flight => flight.Departure == departure, flightIn);
        }
        #endregion

        

    }
}
