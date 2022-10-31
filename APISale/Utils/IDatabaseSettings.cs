
namespace APISale.Utils
{
    public interface IDatabaseSettings
    {
        string SaleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
