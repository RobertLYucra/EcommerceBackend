using EcommerceBackend.Domain;
using EcommerceBackend.Helpers;
using EcommerceBackend.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcommerceBackend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MongoConnections _mongoConnections;
        private IMongoCollection<Category> _collections;

        public CategoryRepository(IConfiguration configuration)
        {
            _mongoConnections = new MongoConnections(configuration);
            _collections = _mongoConnections.database.GetCollection<Category>("CategoryProduct");
        }
        public  async Task<List<Category>> GetAllCategories()
        {
            return await _collections.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            var filter = Builders<Category>.Filter.Eq(x=>x.CategoryId, categoryId);
            return await _collections.Find(filter).SingleOrDefaultAsync();
        }
        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                await _collections.InsertOneAsync(category);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCateogry(Guid categoryId)
        {
            try
            {
                var filter = Builders<Category>.Filter.Eq(c => c.CategoryId, categoryId);
                await _collections.DeleteOneAsync(filter);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategory(Category product, Guid productId)
        {
            var filter = Builders<Category>.Filter.Eq(x => x.CategoryId, productId);
            var result = await _collections.ReplaceOneAsync(filter, product);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return true;
            }
            return false;

        }
    }
}
