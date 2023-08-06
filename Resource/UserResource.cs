using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;

namespace EcommerceBackend.Resource
{
    public class UserResource : IUserResource
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserMapper _userMapper;
        public UserResource(IUserRepository userRepository, IConfiguration configuration, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userMapper = userMapper;
        }

        public async Task<List<UserResponse>> GetAllEmployees()
        {
            try
            {
                var employees = await _userRepository.GetAllEmployees();
                var listaEmployeesResponse = employees.Select(employee => _userMapper.UserResponseFormato(employee)).ToList();
                return listaEmployeesResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<UserResponse> GetById(Guid id)
        {
            try
            {
                var employee = await _userRepository.GetEmployeeById(id);
                if (employee != null)
                {
                    var employeeFound =  _userMapper.UserResponseFormato(employee);
                    return employeeFound;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEmployee(Guid employeeId)
        {
            try
            {
                var userCurrent = await _userRepository.GetEmployeeById(employeeId);
                if (userCurrent == null) return false;

                userCurrent.IsActive = false;
                var updatesOrEliminated = await _userRepository.UpdateEmployee(userCurrent, employeeId);
                return updatesOrEliminated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserResponse> CreateEmployee(UserParams userParams)
        {
            try
            {
                var existingEmployee = await _userRepository.GetEmployeeByNameDni(userParams.Name,userParams.LastName,userParams.DNI);
                if (existingEmployee != null)
                {
                    existingEmployee.Rol = userParams.Rol;
                    existingEmployee.Email = userParams.Email;
                    existingEmployee.Telephone = userParams.Telephone;
                    existingEmployee.IsActive = userParams.IsActive;

                    var updated = await _userRepository.UpdateEmployee(existingEmployee, existingEmployee.UserId);
                    return updated ? _userMapper.UserResponseFormato(existingEmployee) : null;
                }

                var ExistActive = GetAllEmployees().Result.Where(x=>x.Name == userParams.Name && x.LastName == userParams.LastName && x.DNI == userParams.DNI).ToList();
                if (ExistActive.Count()==0)
                {
                    var user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Rol = userParams.Rol,
                        Credentials = userParams.Credentials,
                        Name = userParams.Name,
                        LastName = userParams.LastName,
                        Email = userParams.Email,
                        DNI = userParams.DNI,
                        Telephone = userParams.Telephone,
                        IsActive = userParams.IsActive,
                        Created = DateTime.Now
                    };

                    var created = await _userRepository.CreateEmployee(user);
                    return created ? _userMapper.UserResponseFormato(user) : null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserResponse> UpdateEmployee(UserParams userParams, Guid employeeId)
        {
            try
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
                if (userParams.IsActive != null)
                {
                    existingEmployee.IsActive = userParams.IsActive;
                }

                var updated = await _userRepository.UpdateEmployee(existingEmployee, employeeId);
                return updated ? _userMapper.UserResponseFormato(existingEmployee) : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
