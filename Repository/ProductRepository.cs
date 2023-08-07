using EcommerceBackend.Domain;
using EcommerceBackend.Helpers;
using EcommerceBackend.Repository.Abstract;
using MongoDB.Driver;

namespace EcommerceBackend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoConnections _mongoConnections;
        private IMongoCollection<Product> Collection;
        public ProductRepository(IConfiguration configuration)
        {
            _mongoConnections = new MongoConnections(configuration);
            Collection = _mongoConnections.database.GetCollection<Product>("Products");
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var filter = Builders<Product>.Filter.Eq(x => x.IsDeleted, false);
            var employees = await Collection.Find(filter).ToListAsync();
            return employees;
        }

        public async Task<Product> GetProductByID(Guid productId)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(x => x.ProductId, productId);
                var product = await Collection.Find(filter).SingleOrDefaultAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                await Collection.InsertOneAsync(product);
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateProduct(Product product, Guid productId)
        {
            try
            {
                var productExiste = Builders<Product>.Filter.Eq(x => x.ProductId, productId);
                var updated = await Collection.ReplaceOneAsync(productExiste, product);
                if (updated.IsAcknowledged && updated.ModifiedCount > 0)
                {
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetProductByName(string name, string categoria)
        {
            var filter = Builders<Product>.Filter.And(
                Builders<Product>.Filter.Eq(x=>x.ProductName, name),
                Builders<Product>.Filter.Eq(x=>x.Categoria, categoria));
            return await Collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
