using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcommerceBackend.Domain
{
    public class Venta
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Guid VentaId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
        public List<CartItem> Products { get; set; }
    }

    public class CartItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }  
        public double  PriceTotal { get; set; }
    }
}
