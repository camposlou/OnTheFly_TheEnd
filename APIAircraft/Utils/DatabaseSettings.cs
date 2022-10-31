namespace APIAircraft.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        #region Method
        public string AircraftCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DeletedAircraftCollectionName { get; set; }
        #endregion
    }
}
