namespace APIAirport.Utils
{
    public interface IDatabaseSettings
    {
        string AirportCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
    }
}