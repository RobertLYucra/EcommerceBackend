using EcommerceBackend.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllEmployees();

        public Task<User> GetEmployeeById(int employeeId);
        public Task<bool> DeleteEmployee(int employeeId);
        public Task<bool> CreateEmployee(User employee);
        public Task<bool> UpdateEmployee(User employee, int userId);
    }
}
