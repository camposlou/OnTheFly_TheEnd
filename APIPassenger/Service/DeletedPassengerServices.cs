using APIPassenger.Utils;
using Domain.Models;
using MongoDB.Driver;
using System.Collections.Generic;


namespace APIPassenger.Services
{
    public class DeletedPassengerServices
    {
        private readonly IMongoCollection<DeletedPassenger> _deletedPassenger;
        public DeletedPassengerServices(IDatabaseSettings settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.DatabaseName);
            _deletedPassenger = database.GetCollection<DeletedPassenger>(settings.DeletedPassengerCollectionName);
        }
        public DeletedPassenger Create(DeletedPassenger deletedPassenger)
        {
            _deletedPassenger.InsertOne(deletedPassenger);
            return deletedPassenger;
        }
        public List<DeletedPassenger> Get() => _deletedPassenger.Find<DeletedPassenger>(deletedPassenger => true).ToList();
        public DeletedPassenger Get(string cpf) => _deletedPassenger.Find<DeletedPassenger>(deletedPassenger => deletedPassenger.CPF == cpf).FirstOrDefault();
    }
}
