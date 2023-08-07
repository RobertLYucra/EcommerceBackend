using EcommerceBackend.Domain;
using EcommerceBackend.Helpers;
using EcommerceBackend.Repository.Abstract;
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
            var filter = Builders<User>.Filter.Eq(x => x.IsActive, true);
            var employees = await Collection.Find(filter).ToListAsync();
            return employees;
        }

        public async Task<User> GetEmployeeByNameDni(string name, string lastName, string dni)
        {
            var filter = Builders<User>.Filter.And(
               Builders<User>.Filter.Eq(x => x.Name, name),
               Builders<User>.Filter.Eq(x => x.LastName, lastName),
               Builders<User>.Filter.Eq(x => x.DNI, dni),
               Builders<User>.Filter.Eq(x => x.IsActive, false)
               );
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetEmployeeById(Guid employeeId)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.UserId,employeeId);
                return await Collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public async Task<bool> UpdateEmployee(User employee, Guid userId)
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
