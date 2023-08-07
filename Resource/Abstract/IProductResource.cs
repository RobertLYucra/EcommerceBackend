using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Resource.Abstract
{
    public interface IProductResource
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(Guid productId);
        Task<ProductResponse> CreateProduct(ProductParams productParams);
        Task<ProductResponse> UpdateProduct(ProductParams productParams,Guid productId);
        Task<ProductResponse> DeleteProduct(Guid productId);
    }
}
