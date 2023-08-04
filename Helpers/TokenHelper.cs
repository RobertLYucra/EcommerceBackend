using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Repository.Abstract;
using System.Security.Claims;

namespace EcommerceBackend.Helpers
{
    public class TokenHelper
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        private readonly IUserRepository _userRepository;
        public TokenHelper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public  TokenValidationResponse ValidateToken(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null) return new TokenValidationResponse(false, "Ingresar token...", null);
           
            if (claimsIdentity.Claims.Count() == 0) return new TokenValidationResponse(false, "Token inválido o nulo", null);
       
            var usernameClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (usernameClaim == null) return new TokenValidationResponse(false, "Token inválido, reclamo 'username' no encontrado", null);
     
            var username = usernameClaim.Value;
            var user = _userRepository.GetAllEmployees().Result.SingleOrDefault(x => x.Credentials.username == username);

            if (user == null) return new TokenValidationResponse(false, "Usuario no encontrado en la base de datos", null);

            var userResponse = new UserResponse
            {
                UserId = user.UserId,
                Rol = user.Rol,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                DNI = user.DNI,
                Telephone = user.Telephone,
                Status = user.Status,
                Created = user.Created
            };

            return new TokenValidationResponse(true, "Token válido", userResponse);
        }

        public class TokenValidationResponse
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
            public UserResponse User { get; set; }

            public TokenValidationResponse(bool isValid, string message, UserResponse user)
            {
                IsValid = isValid;
                Message = message;
                User = user;
            }
        }

    }
}
