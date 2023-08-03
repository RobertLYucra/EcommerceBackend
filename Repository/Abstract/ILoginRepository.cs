using SistemaEncomienda.Domain;

namespace SistemaEncomienda.Repository.Abstract
{
    public interface ILoginRepository
    {
        Task<User> GetUser(UserLogin userLogin);
    }
}
