using SistemaEncomienda.Contracts.Response;

namespace EcommerceBackend.Resource.Abstract
{
    public interface IUserResource
    {
        Task<List<UserResponse>> GetAllEmployees();
    }
}
