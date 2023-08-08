using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;

namespace EcommerceBackend.Resource
{
    public class VentaResource : IVentaResource
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVentaMapper _ventaMapper;
        private readonly IProductRepository _productRepository;

        public VentaResource(
            IVentaRepository ventaRepository,
            IUserRepository userRepository,
            IVentaMapper ventaMapper,
            IProductRepository productRepository)
        {
            _ventaRepository = ventaRepository;
            _userRepository = userRepository;
            _ventaMapper = ventaMapper;
            _productRepository = productRepository;
        }
        public async Task<VentaResponse> CreateVenta(VentaParams ventaParams)
        {
            var venta = new Venta
            {
                VentaId = Guid.NewGuid(),
                UserId = ventaParams.UserId,
                Created = DateTime.UtcNow,
                Products = ventaParams.Products,
            };

            var user = await _userRepository.GetEmployeeById(ventaParams.UserId);

            foreach (var item in ventaParams.Products)
            {
                var product = await _productRepository.GetProductByID(item.ProductId);
                if (product.Stock < item.Quantity)
                {
                    throw new Exception("La cantidad pedido del producto debe ser mayor o igual al stock");
                }
                product.Stock = product.Stock - item.Quantity; 
                var updated = await _productRepository.UpdateProduct(product, item.ProductId);
                if (!updated) throw new Exception("Error al cambiar el stock");
            }
            var created = await _ventaRepository.CreateVenta(venta);
            return created?  await _ventaMapper.MapToVentaResponse(venta, ventaParams.Products, user):null ;
        }

        public async Task<List<VentaResponse>> GetAllVentas()
        {
            var ventas = await _ventaRepository.AllVentas();

            var listaResponse = await Task.WhenAll(ventas.Select(async venta =>
            {
                var user = await _userRepository.GetEmployeeById(venta.UserId);
                return await _ventaMapper.MapToVentaResponse(venta, venta.Products, user);
            }).ToList());

            return listaResponse.ToList();
        }
    }
}
