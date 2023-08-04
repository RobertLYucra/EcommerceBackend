using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;
using SistemaEncomienda.Contracts.Response;

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

        public  List<UserResponse> GetAllEmployees()
        {
            var employees =  _userRepository.GetAllEmployees();
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
            }catch(Exception ex)
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
    }
}
