using EcommerceBackend.Domain;
using EcommerceBackend.Helpers;
using EcommerceBackend.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcommerceBackend.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly MongoConnections _mongoConnections;
        private IMongoCollection<Venta> _Collection;
        public VentaRepository(IConfiguration configuration)
        {
            _mongoConnections = new MongoConnections(configuration);
            _Collection = _mongoConnections.database.GetCollection<Venta>("Venta");
        }
        public async Task<bool> CreateVenta(Venta venta)
        {
            try
            {
                Venta ventas = venta;
                await _Collection.InsertOneAsync(ventas);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Venta>> AllVentas()
        {
            return await _Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
