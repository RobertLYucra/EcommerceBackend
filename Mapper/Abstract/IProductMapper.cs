using EcommerceBackend.Contracts;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Mapper.Abstract
{
    public interface IProductMapper
    {
        public ProductResponse ProductResponse(Product product);
    }
}
