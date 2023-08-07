using EcommerceBackend.Contracts;
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
                UserName = user.Credentials.username,
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
