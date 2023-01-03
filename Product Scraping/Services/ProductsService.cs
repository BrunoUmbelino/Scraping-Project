using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product_Scraping.Models;
using Product_Scraping.Models.Database;

namespace Product_Scraping.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductsService(IOptions<FreeFoodFactsScrapingDatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _products = database.GetCollection<Product>(dbSettings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> GetAllAsync() => await _products.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(long code) => await _products.Find(p => p.Code == code).FirstOrDefaultAsync();

        public async Task CreateManyAsync(List<Product> products)
        {
            _products.DeleteMany((_) => true);
            await _products.InsertManyAsync(products);
        }
    }
}
