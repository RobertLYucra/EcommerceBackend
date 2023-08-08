using EcommerceBackend.Contracts;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository.Abstract;

namespace EcommerceBackend.Mapper
{
    public class VentaMapper : IVentaMapper
    {
        private readonly IProductRepository _productRepository;
        public VentaMapper( IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }
        public async Task<VentaResponse> MapToVentaResponse(Venta venta, List<CartItem> Products, User user)
        {

            var productsVenta = new List<ProductsVenta>();
            foreach (var productVenta in Products)
            {
                var prod = await _productRepository.GetProductByID(productVenta.ProductId);
                var productVentaItem = new ProductsVenta
                {
                    ProductId = productVenta.ProductId,
                    ProductName = prod.ProductName,
                    Image = prod.Imagen,
                    Price = productVenta.Price,
                    Quantity = productVenta.Quantity,
                    PriceTotal = productVenta.PriceTotal
                };
                productsVenta.Add(productVentaItem);
            }

            var ventaResponse = new VentaResponse
            {
                VentaId = venta.VentaId,
                EmployeeId = venta.UserId,
                EmployeeName = user.Name,
                EmployeeLastName = user.LastName,
                Created = venta.Created,
                Products = productsVenta
            };

            return ventaResponse;
        }
    }
}
