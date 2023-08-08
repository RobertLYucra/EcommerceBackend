using EcommerceBackend.Contracts;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Mapper.Abstract
{
    public interface IVentaMapper
    {
        public Task<VentaResponse> MapToVentaResponse(Venta venta, List<CartItem> Products, User user);
    }
}
