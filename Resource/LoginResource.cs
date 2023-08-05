using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository.Abstract;
using EcommerceBackend.Resource.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceBackend.Resource
{
    public class LoginResource : ILoginResource
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserMapper _userMapper;
        public LoginResource(ILoginRepository loginRepository, IConfiguration configuration, IUserMapper userMapper)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _userMapper = userMapper;
        }

        public UserTokenResponse GetUserLogin(UserLogin userLogin)
        {
            var currentUser = _loginRepository.GetUser(userLogin).Result;

            if (currentUser != null)
            {
                var token = GenerateToken(currentUser);
                var userTokenResponse = new UserTokenResponse
                {
                    UserResponse = _userMapper.UserResponseFormato(currentUser),
                    Token = token
                };

                return userTokenResponse;
            }
            return null;
        }

        private string GenerateToken(User user)
        {
            var securityKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKeys, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Credentials.username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Role,user.Rol),
                new Claim("DNI",user.DNI),
                new Claim("IsActive",user.IsActive.ToString())
            };
            var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials
);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
