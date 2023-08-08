using EcommerceBackend.Domain;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EcommerceBackend.Contracts.Params
{
    public class VentaParams
    {
        public Guid UserId { get; set; }
        public List<CartItem> Products { get; set; }

        public void Validate()
        {
            if(Products.Count <= 0) {
                throw new ArgumentException("La lista de productos está vacío");
            }
        }
    }
}
