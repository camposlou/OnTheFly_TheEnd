using APICompany.Utils;
using Domain.Models;
using MongoDB.Driver;

namespace APICompany.Services
{
    public class DeletedCompanyServices
    {
        private readonly IMongoCollection<DeletedCompany> _deleted;

        public DeletedCompanyServices(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _deleted = database.GetCollection<DeletedCompany>(settings.DeleteCollectionName);
        }

        public void Create(DeletedCompany delete)
        {
            _deleted.InsertOne(delete);
        }
    }
}
