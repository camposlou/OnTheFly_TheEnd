using Domain.Models;
using APIAirport.Utils;
using MongoDB.Driver;
using System.Collections.Generic;

namespace APIAirport.Services
{
    public class AirportServices
    {
        private readonly IMongoCollection<Airport> _airports;

        public AirportServices(IDatabaseSettings settings)
        {
            var airport = new MongoClient(settings.ConnectionString);
            var database = airport.GetDatabase(settings.DataBaseName);
            _airports = database.GetCollection<Airport>(settings.AirportCollectionName);
        }
        //#region Post
        //public async Task<Airport> Create(Airport airport)
        //{
        //   await _airports.InsertOneAsync(airport);
        //    return airport;
        //}
        //#endregion

        //#region Get List
        //public async Task<List<Airport>> Get() =>
        //   await _airports.Find(airport => true).ToListAsync();
        //#endregion

        #region Get Iata
        public Airport GetIata(string iata) =>
             _airports.Find<Airport>(airport => airport.iata == iata).FirstOrDefault();
        #endregion

        #region Get Icao
        public List<Airport> GetByIcao(string icao) =>
           _airports.Find<Airport>(airport => airport.icao == icao).ToList();
        #endregion
              

        #region Get City
        public List<Airport> GetByCity(string city_code) =>
            _airports.Find<Airport>(airport => airport.city_code == city_code).ToList();
        #endregion

        public List<Airport> GetByState(string state) =>
         _airports.Find<Airport>(airport => airport.state == state).ToList();

    }
}