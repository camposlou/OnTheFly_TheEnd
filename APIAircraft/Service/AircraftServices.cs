using APIAircraft.Utils;
using Domain.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIAircraft.Services
{
    public class AircraftServices
    {
        #region Attribute
        private readonly IMongoCollection<Aircraft> _aircraftService;
        #endregion

        #region Method
        public AircraftServices(IDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseName);
            _aircraftService = database.GetCollection<Aircraft>(settings.AircraftCollectionName);
        }
        #endregion

        #region Insert AirCraft
        public Aircraft CreateAircraft(Aircraft aircraft)
        {
            _aircraftService.InsertOne(aircraft);
            return aircraft;
        }
        #endregion

        #region Read All AirCraft
        public List<Aircraft> GetAllAircraft() => _aircraftService.Find<Aircraft>(aircraft => true).ToList();
        #endregion

        #region Read AirCraft One From RAB
        public Aircraft GetByAircraft(string rab) => _aircraftService.Find<Aircraft>(aircraft => aircraft.RAB == rab).FirstOrDefault();
        #endregion

        #region Read AirCraft One From CNPJ
        //public List<AirCraft> GetByCNPJ(string cnpj) => _aircraft.Find<AirCraft>(aircraft => aircraft.CNPJ == cnpj).ToList();
        #endregion

        #region Update AirCraft
        public void UpdateAircraft(string rab, Aircraft aircraftIn) => _aircraftService.ReplaceOne(aircraft => aircraft.RAB == rab, aircraftIn);
        #endregion

        #region Delete AirCraft
        public void RemoveAircraft(Aircraft aircraftIn) => _aircraftService.DeleteOne(aircraft => aircraft.RAB == aircraftIn.RAB);
        #endregion

        #region Get API Company consumindo api
        public async Task<Company> GetApiCompany(string cnpj)
        {
            Company company;
            using (HttpClient _companyClient = new HttpClient())
            {
                HttpResponseMessage response = await _companyClient.GetAsync("caminho api" + cnpj + "/json/"); // Adicionar o caminho
                var companyJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return company = JsonConvert.DeserializeObject<Company>(companyJson);
                else
                    return null;
            }
        }
        #endregion
    }
}











