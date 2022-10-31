using APIPassenger.Utils;
using Domain.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace APIPassenger.Services
{
    public class RestrictedServices
    {
        private readonly IMongoCollection<Restricted> _restricted;

        public RestrictedServices(IDatabaseSettings settings)
        {
            var restricted = new MongoClient(settings.ConnectionString);
            var database = restricted.GetDatabase(settings.DatabaseName);
            _restricted = database.GetCollection<Restricted>(settings.RestrictedCollectionName);
        }
        public Restricted Create(Restricted restrictedpassenger)
        {
            _restricted.InsertOne(restrictedpassenger);
            return restrictedpassenger;
        }
        public List<Restricted> Get() => _restricted.Find<Restricted>(restricted => true).ToList();
        public Restricted Get(string cpf) => _restricted.Find<Restricted>(restricted => restricted.CPF == cpf).FirstOrDefault();
    }
}
