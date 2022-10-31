
namespace APISale.Utils
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string SaleCollectionName { get; set; }
    }
}
