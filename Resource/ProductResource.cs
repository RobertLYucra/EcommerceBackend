using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;

namespace EcommerceBackend.Resource
{
    public class ProductResource : IProductResource
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductMapper _productMapper;
        public ProductResource(IProductRepository productRepository, IProductMapper productMapper)
        {
            _productRepository = productRepository;
            _productMapper = productMapper;
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            var productResponse = products.Select(x => _productMapper.ProductResponse(x)).ToList();
            return productResponse;
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetProductByID(productId);
                return _productMapper.ProductResponse(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductResponse> CreateProduct(ProductParams productParams)
        {
            try
            {
                //Verificando si existe ya ese producto en la DB;
                var productExist = await _productRepository.GetProductByName(productParams.ProductName, productParams.Categoria);
                if (productExist != null)
                {
                    productExist.Updated = DateTime.Now;
                    productExist.IsDeleted = false;
                    productExist.Stock = productParams.Stock;
                    productExist.Price = productParams.Price;
                    productExist.Description = productParams.Description;
                    productExist.Imagen = productParams.Imagen;

                    var createdOrUpdated = await _productRepository.UpdateProduct(productExist, productExist.ProductId);
                    return createdOrUpdated ? _productMapper.ProductResponse(productExist) : null;
                }
                //Insertando si es nuevo
                var product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = productParams.ProductName,
                    Price = productParams.Price,
                    Stock = productParams.Stock,
                    Categoria = productParams.Categoria,
                    Imagen = productParams.Imagen,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Description = productParams.Description,
                    IsDeleted = false
                };

                var created = await _productRepository.CreateProduct(product);
                return created ? _productMapper.ProductResponse(product) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductResponse> UpdateProduct(ProductParams productParams, Guid productId)
        {
            try
            {
                var product = await _productRepository.GetProductByID(productId);
                if (product == null) return null;

                product.ProductName = productParams.ProductName;
                product.Price = productParams.Price;
                product.Stock = productParams.Stock;
                product.Categoria = productParams.Categoria;
                product.Imagen = productParams.Imagen;
                product.Updated = DateTime.Now;
                product.Description = productParams.Description;

                var updated = await _productRepository.UpdateProduct(product, productId);
                return updated ? _productMapper.ProductResponse(product) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductResponse> DeleteProduct(Guid productId)
        {
            try
            {
                var user = await _productRepository.GetProductByID(productId);
                if (user == null) return null;

                user.IsDeleted = true;
                user.Updated = DateTime.Now;

                var deleted = await _productRepository.UpdateProduct(user, productId);
                return deleted ? _productMapper.ProductResponse(user) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductResponse>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetAllProducts();
            var productResponse = products.Where(x=>x.Categoria == category).ToList();
            return productResponse.Select(x=> _productMapper.ProductResponse(x)).ToList();  
        }
    }
}
