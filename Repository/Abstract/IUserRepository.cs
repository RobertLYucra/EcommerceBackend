using SistemaEncomienda.Contracts.Response;
using SistemaEncomienda.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllEmployees();

        public Task<User> GetEmployeeById(int employeeId);
        public Task<User> UpdateEmployee(User employee);
        public Task<User> DeleteEmployee(int employeeId);
        public Task<User> CreateEmployee(User employee);
    }
}
