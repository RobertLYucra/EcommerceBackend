using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;

namespace EcommerceBackend.Mapper
{
    public class UserMapper : IUserMapper
    {
        public UserResponse UserResponseFormato(User user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                Rol = user.Rol,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                DNI = user.DNI,
                Telephone = user.Telephone,
                IsActive = user.IsActive,
                Created = user.Created
            };
        }
    }
}
