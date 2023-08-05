namespace EcommerceBackend.Contracts.Response
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string Rol { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string? Telephone { get; set; }
        public DateTime? Created { get; set; }
        public bool IsActive { get; set; }
    }
}
