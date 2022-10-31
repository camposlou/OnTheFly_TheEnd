namespace APIFlight.Utils
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }              
        string ConnectionString { get; set; }       
        string FlightCollectionName { get; set; }      
       
       
       
    }
}
