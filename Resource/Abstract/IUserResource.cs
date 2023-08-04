using SistemaEncomienda.Contracts.Response;

namespace EcommerceBackend.Resource.Abstract
{
    public interface IUserResource
    {
        List<UserResponse> GetAllEmployees();
        UserResponse GetById(int id);
    }
}
