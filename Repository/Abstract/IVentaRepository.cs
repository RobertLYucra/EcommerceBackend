using EcommerceBackend.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface IVentaRepository
    {
        Task<bool> CreateVenta(Venta venta);
        public  Task<List<Venta>> AllVentas();
    }
}
