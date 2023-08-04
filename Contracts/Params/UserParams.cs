using EcommerceBackend.Domain;

namespace EcommerceBackend.Contracts.Params
{
    public class UserParams
    {
        public int? UserId { get; set; }
        public string Rol { get; set; }
        public UserLogin ? Credentials { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string Telephone { get; set; }
        public bool Status { get; set; }
    }
}
