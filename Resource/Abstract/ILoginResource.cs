using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;

namespace EcommerceBackend.Resource.Abstract
{
    public interface ILoginResource
    {
        public UserTokenResponse GetUserLogin(UserLogin userLogin);
    }
}
