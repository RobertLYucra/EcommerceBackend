using EcommerceBackend.Domain;
using EcommerceBackend.Helpers;
using EcommerceBackend.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;

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
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<User> GetEmployeeById(int employeeId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.UserId, employeeId);
            return await Collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
        public async Task<bool> CreateEmployee(User employee)
        {
            try
            {
                await Collection.InsertOneAsync(employee);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.UserId, employeeId);
                await Collection.DeleteOneAsync(filter);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployee(User employee, int userId)
        {
            try
            {
                employee.Credentials.Validate();
                var filter = Builders<User>.Filter.Eq(x => x.UserId, userId);
                var result = await Collection.ReplaceOneAsync(filter, employee);
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
