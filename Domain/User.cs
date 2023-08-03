using MongoDB.Bson;

namespace SistemaEncomienda.Domain
{
    public class User
    {
        public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public string Rol { get; set; }
        public UserLogin Credentials { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string? Telephone { get; set; }
        public DateTime? Created { get; set; }
        public bool Status { get; set; }
    }

    public class UserLogin
    {
        public string username { get; set; }
        public string password { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is empty...");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is empty...");
            }
        }
    }
}
