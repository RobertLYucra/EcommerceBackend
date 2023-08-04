using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;

namespace EcommerceBackend.Resource
{
    public class UserResource : IUserResource
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserResource(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public List<UserResponse> GetAllEmployees()
        {
            var employees = _userRepository.GetAllEmployees();
            var filteredEmployees = employees.Result.ToList().Where(x => x.Status != false);

            var listaEmployeesResponse = filteredEmployees.Select(employee => new UserResponse
            {
                UserId = employee.UserId,
                Rol = employee.Rol,
                Name = employee.Name,
                LastName = employee.LastName,
                Email = employee.Email,
                DNI = employee.DNI,
                Telephone = employee.Telephone,
                Status = employee.Status,
                Created = employee.Created
            }).ToList();

            return listaEmployeesResponse;
        }

        public UserResponse GetById(int id)
        {
            try
            {
                var employee = _userRepository.GetEmployeeById(id).Result;
                var employeeResponse = new UserResponse
                {
                    UserId = employee.UserId,
                    Rol = employee.Rol,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    DNI = employee.DNI,
                    Telephone = employee.Telephone,
                    Status = employee.Status,
                    Created = employee.Created
                };
                return employeeResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            var eliminated = _userRepository.DeleteEmployee(employeeId).Result;
            if (eliminated) return true;
            return false;
        }

        public async Task<UserResponse> UpdateEmployee(UserParams userParams, int employeeId)
        {
            User existingEmployee = await _userRepository.GetEmployeeById(employeeId);
            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.Rol = userParams.Rol;
            existingEmployee.Name = userParams.Name;
            existingEmployee.LastName = userParams.LastName;
            existingEmployee.Email = userParams.Email;
            existingEmployee.DNI = userParams.DNI;
            existingEmployee.Telephone = userParams.Telephone;
            existingEmployee.Status = userParams.Status;
            existingEmployee.Created = DateTime.UtcNow;


            var updated = await _userRepository.UpdateEmployee(existingEmployee, employeeId);
            if (updated)
            {
                return new UserResponse
                {
                    UserId = existingEmployee.UserId,
                    Rol = existingEmployee.Rol,
                    Name = existingEmployee.Name,
                    LastName = existingEmployee.LastName,
                    Email = existingEmployee.Email,
                    DNI = existingEmployee.DNI,
                    Telephone = existingEmployee.Telephone,
                    Status = existingEmployee.Status,
                    Created = existingEmployee.Created
                };
            }
            return null;
        }
    }
}
