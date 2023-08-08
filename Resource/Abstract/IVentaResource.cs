using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Resource.Abstract
{
    public interface IVentaResource
    {
        public Task<List<VentaResponse>> GetAllVentas();
        public Task<VentaResponse> CreateVenta(VentaParams ventaParams);
    }
}
