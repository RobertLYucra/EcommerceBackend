using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Contracts.Response;

namespace EcommerceBackend.Resource.Abstract
{
    public interface IUserResource
    {
        List<UserResponse> GetAllEmployees();
        UserResponse GetById(int id);
        bool DeleteEmployee(int employeeId);
        Task<UserResponse> UpdateEmployee(UserParams userParams, int employeeId);
    }
}
