using EcommerceBackend.Contracts;
using EcommerceBackend.Contracts.Params;

namespace EcommerceBackend.Resource.Abstract
{
    public interface IUserResource
    {
        Task<List<UserResponse>> GetAllEmployees();
        Task<UserResponse> GetById(Guid id);
       Task<bool> DeleteEmployee(Guid employeeId);
        Task<UserResponse> CreateEmployee(UserParams userParams);
        Task<UserResponse> UpdateEmployee(UserParams userParams, Guid employeeId);
    }
}
