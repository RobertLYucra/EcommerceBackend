using MongoDB.Bson;
using MongoDB.Driver;
using SistemaEncomienda.Domain;
using SistemaEncomienda.Helpers;
using SistemaEncomienda.Repository.Abstract;

namespace SistemaEncomienda.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MongoConnections _mongoConnections;
        private IMongoCollection<User> Collection;
        public LoginRepository( IConfiguration configuration)
        {
              _mongoConnections = new MongoConnections(configuration);
             Collection = _mongoConnections.database.GetCollection<User>("User");
        }

        public async Task<User> GetUser(UserLogin userLogin)
        {
            try
            {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(x => x.Credentials.username, userLogin.username),
                Builders<User>.Filter.Eq(x => x.Credentials.password, userLogin.password)
                );
            var currentUser = await Collection.Find(filter).FirstOrDefaultAsync();
            if(currentUser != null ) return currentUser;

            return null;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
