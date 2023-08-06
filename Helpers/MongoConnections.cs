using MongoDB.Driver;

namespace EcommerceBackend.Helpers
{
    public class MongoConnections
    {
        private readonly IConfiguration _configuration;
        public MongoClient client;
        public IMongoDatabase database;

        public MongoConnections(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("MongoDBAtlasConnectionString");
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            client = new MongoClient(settings);
            database = client.GetDatabase("EcommerceBackend");
        }
    }
}

