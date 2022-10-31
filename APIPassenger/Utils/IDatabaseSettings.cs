namespace APIPassenger.Utils
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string PassengerCollectionName { get; set; }
        string DeletedPassengerCollectionName { get; set; }
        string RestrictedCollectionName { get; set; }
    }
}
