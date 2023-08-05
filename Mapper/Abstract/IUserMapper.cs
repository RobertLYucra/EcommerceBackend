using EcommerceBackend.Contracts.Params;
using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Mapper.Abstract
{
    public interface IUserMapper
    {
        public UserResponse UserResponseFormato(User user);
    }
}
