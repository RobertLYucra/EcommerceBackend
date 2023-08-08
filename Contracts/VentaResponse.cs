using EcommerceBackend.Domain;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EcommerceBackend.Contracts
{
    public class  VentaResponse
    {
        public Guid VentaId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime Created { get; set; }
        public List<ProductsVenta> Products { get; set; }
    }

    public class ProductsVenta
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceTotal { get; set; }
    }
}
