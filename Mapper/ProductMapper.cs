using EcommerceBackend.Contracts;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;

namespace EcommerceBackend.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public ProductResponse ProductResponse(Product product)
        {
            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock,
                Categoria = product.Categoria,
                Imagen = product.Imagen,
                Created = product.Created,
                Updated = product.Updated,
                Description = product.Description,
                IsDeleted = product.IsDeleted,
            };
        }
    }
}
