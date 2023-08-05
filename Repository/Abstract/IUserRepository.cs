using EcommerceBackend.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllEmployees();

        public Task<User> GetEmployeeById(Guid employeeId);
        public Task<bool> DeleteEmployee(Guid employeeId);
        public Task<bool> CreateEmployee(User employee);
        public Task<bool> UpdateEmployee(User employee, Guid userId);
        Task<User> GetEmployeeByNameDni(string name, string lastName, string dni);
    }
}
