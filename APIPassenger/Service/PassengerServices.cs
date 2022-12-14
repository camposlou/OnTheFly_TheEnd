using APIPassenger.Utils;
using Domain.Models;
using MongoDB.Driver;
using System.Collections.Generic;


namespace APIPassenger.Services
{
    public class PassengerServices
    {
        private readonly IMongoCollection<Passenger> _passenger;

        public PassengerServices(IDatabaseSettings settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.DatabaseName);
            _passenger = database.GetCollection<Passenger>(settings.PassengerCollectionName);
        }
        public Passenger Create(Passenger passenger)
        {
            _passenger.InsertOne(passenger);
            return passenger;
        }
        public void Put(string cpf, Passenger passengerIn)
        {
            _passenger.ReplaceOne(passenger => passenger.CPF == cpf, passengerIn);
        }
        public List<Passenger> Get() => _passenger.Find<Passenger>(passenger => true).ToList();
        public Passenger Get(string cpf) => _passenger.Find<Passenger>(passenger => passenger.CPF == cpf).FirstOrDefault();
        public void Remove(Passenger passengerIn) => _passenger.DeleteOne(passenger => passenger.CPF == passengerIn.CPF);
    }
}
