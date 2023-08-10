using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;
using System.Security.Claims;

namespace EcommerceBackend.Resource.Abstract
{
    public interface ILoginResource
    {
        public UserTokenResponse GetUserLogin(UserLogin userLogin);
        public UserTokenResponse ValidateToken(ClaimsIdentity claimsIdentity);
    }
}
