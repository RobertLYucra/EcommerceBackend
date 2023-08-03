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

        public Task<User> GetEmployeeById(int employeeId)
        {
            throw new NotImplementedException();
        }
        public Task<User> CreateEmployee(User employee)
        {
            return null;
        }

        public Task<User> DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateEmployee(User employee)
        {
            throw new NotImplementedException();
        }
    }
}
