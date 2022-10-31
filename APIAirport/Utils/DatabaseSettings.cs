namespace APIAirport.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string AirportCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }

}
