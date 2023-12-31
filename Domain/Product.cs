﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcommerceBackend.Domain
{
    public class Product
    {
        [BsonId]
        public ObjectId id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
