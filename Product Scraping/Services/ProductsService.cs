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

        public async Task<List<Product>> GetAllAsync(int pageIndex, int pageSize)
        {
            var skipNum = pageIndex <= 1 ? 0 : (pageIndex-1) * pageSize;
            return await _products.Find(_ => true).Skip(skipNum).Limit(pageSize).ToListAsync();
        }
            

        public async Task<Product?> GetAsync(long code) => await _products.Find(p => p.Code == code).FirstOrDefaultAsync();

        public async Task<List<Product>> CreateManyAsync(List<Product> products)
        {
            foreach (var product in products)
            {
                product.Status = Status.Imported;
                product.ImportedT = DateTime.UtcNow;
                await _products.InsertOneAsync(product);
            }

            return products;
        }
    }
}
