using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcommerceBackend.Domain
{
    public class Category
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Imagen { get; set; }
        public string? CategoryDescription { get; set; }
        public DateTime Created { get; set; }
    }
}
