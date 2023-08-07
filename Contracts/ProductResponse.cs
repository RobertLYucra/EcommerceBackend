using EcommerceBackend.Domain;
using MongoDB.Bson;

namespace EcommerceBackend.Contracts
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int  Stock { get; set; }
        public double Price { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
