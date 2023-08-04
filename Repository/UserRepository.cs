using EcommerceBackend.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using SistemaEncomienda.Domain;
using SistemaEncomienda.Helpers;

namespace EcommerceBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoConnections _mongoConnections;
        private IMongoCollection<User> Collection;
        public UserRepository(IConfiguration configuration)
        {
            _mongoConnections = new MongoConnections(configuration);
            Collection = _mongoConnections.database.GetCollection<User>("User");
        }

        public async Task<List<User>> GetAllEmployees()
        {
            return await  Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<User> GetEmployeeById(int employeeId)
        {
            var filter = Builders<User>.Filter.Eq(x=>x.UserId,employeeId);
            return await Collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
        public Task<User> CreateEmployee(User employee)
        {
            return null;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            try
            {
            var filter = Builders<User>.Filter.Eq(x => x.UserId, employeeId);
            await Collection.DeleteOneAsync(filter);

            return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public Task<User> UpdateEmployee(User employee)
        {
            throw new NotImplementedException();
        }
    }
}
