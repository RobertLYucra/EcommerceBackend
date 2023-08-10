using EcommerceBackend.Contracts;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;
using System.Security.Claims;

namespace EcommerceBackend.Helpers
{
    public class TokenHelper
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

       

        public   static TokenValidationResponse ValidateToken(ClaimsIdentity claimsIdentity,IUserRepository userRepository)
        {

            if (claimsIdentity == null) return new TokenValidationResponse(false, "Ingresar token...", null,DateTime.UtcNow);

            if (claimsIdentity.Claims.Count() == 0) return new TokenValidationResponse(false, "Token inválido o nulo",null, DateTime.UtcNow);
       
            var usernameClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (usernameClaim == null) return new TokenValidationResponse(false, "Token inválido, reclamo 'username' no encontrado",null, DateTime.UtcNow);
     
            var username = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = userRepository.GetAllEmployees().Result.FirstOrDefault(x => x.Credentials.username == username);

            var ExpireDate = claimsIdentity.FindFirst(ClaimTypes.Expiration).Value;

            if (user == null) return new TokenValidationResponse(false, "Usuario no encontrado en la base de datos", null, DateTime.UtcNow);

            var userResponse = new UserResponse
            {
                UserId = user.UserId,
                UserName = username,
                Rol = user.Rol,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                DNI = user.DNI,
                Telephone = user.Telephone,
                IsActive = user.IsActive,
                Created = user.Created
            };

            return new TokenValidationResponse(true, "Token válido", userResponse,DateTime.Parse(ExpireDate));
        }

        public class TokenValidationResponse
        {
            public bool IsValid { get; set; }
            public string Message { get; set; }
            public UserResponse User { get; set; }
            public DateTime ExpireDate { get; set; }

            public TokenValidationResponse(bool isValid, string message, UserResponse user,DateTime expireDate)
            {
                IsValid = isValid;
                Message = message;
                User = user;
                ExpireDate = expireDate;
            }
        }

    }
}
