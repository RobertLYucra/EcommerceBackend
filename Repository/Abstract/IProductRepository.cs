using EcommerceBackend.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductByID(Guid productId);
        public Task<bool> CreateProduct(Product product);
        public Task<bool> UpdateProduct(Product product, Guid productId);
        public Task<Product> GetProductByName(string name,string categoria);
    }
}
