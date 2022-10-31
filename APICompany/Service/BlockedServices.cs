using APICompany.Utils;
using Domain.Models;
using MongoDB.Driver;

namespace APICompany.Services
{
    public class BlockedServices
    {
        private readonly IMongoCollection<Blocked> _blocked;

        public BlockedServices(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _blocked = database.GetCollection<Blocked>(settings.BlockedCollectionName);
        }

        public void Create(Blocked block)
        {
            _blocked.InsertOne(block);
        }
        public Blocked Get(string cnpj) => _blocked.Find<Blocked>(blocked => blocked.Cnpj == cnpj).FirstOrDefault();
    }
}

