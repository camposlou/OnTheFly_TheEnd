using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using APICompany.Utils;
using Domain.Models;
using MongoDB.Driver;


namespace APICompany.Services
{
    public class CompanyServices
    {
        private readonly IMongoCollection<Company> _company;

        public CompanyServices(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _company = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public Company Create(Company company)
        {
            _company.InsertOne(company);
            return company;
        }

        public async Task<Aircraft> PostAircraft(Aircraft aircraft)
        {

            using (HttpClient _aircraftClient = new HttpClient())
            {
                JsonContent content = JsonContent.Create(aircraft);
                HttpResponseMessage response = await _aircraftClient.PostAsync("https://localhost:44321/api/Aircraft", content);
                var aircraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return aircraft = JsonSerializer.Deserialize<Aircraft>(aircraftJson);
                else
                    return null;

            }
        }

        public List<Company> Get() => _company.Find<Company>(company => true).ToList();

        public Company Get(string cnpj) => _company.Find<Company>(company => company.Cnpj == cnpj).FirstOrDefault();
        public void Update(string cnpj, Company companyIn)
        {
            _company.ReplaceOne(company => company.Cnpj == cnpj, companyIn);
        }

        public void Remove(Company companyIn) => _company.DeleteOne(company => company.Cnpj == companyIn.Cnpj);

    }
}
