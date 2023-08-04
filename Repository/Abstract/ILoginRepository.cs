
using EcommerceBackend.Domain;

namespace EcommerceBackend.Repository.Abstract
{
    public interface ILoginRepository
    {
        Task<User> GetUser(UserLogin userLogin);
    }
}
