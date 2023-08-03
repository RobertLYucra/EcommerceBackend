using SistemaEncomienda.Contracts.Response;
using SistemaEncomienda.Domain;

namespace SistemaEncomienda.Resource.Abstract
{
    public interface ILoginResource
    {
        public UserTokenResponse GetUserLogin(UserLogin userLogin);
    }
}
