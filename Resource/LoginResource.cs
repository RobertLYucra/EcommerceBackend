using EcommerceBackend.Contracts.Response;
using EcommerceBackend.Domain;
using EcommerceBackend.Helpers;
using EcommerceBackend.Mapper.Abstract;
using EcommerceBackend.Repository;
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
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserMapper _userMapper;
        public LoginResource(ILoginRepository loginRepository, IConfiguration configuration, IUserMapper userMapper, IUserRepository userRepository)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _userMapper = userMapper;
            _userRepository = userRepository;
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
                    Token = token.token,
                    ExpirationToken = token.expiration,
                };

                return userTokenResponse;
            }
            return null;
        }

        public UserTokenResponse ValidateToken(ClaimsIdentity claimsIdentity)
        {
            var rToken = TokenHelper.ValidateToken(claimsIdentity, _userRepository);
            if (!rToken.IsValid) return null;
            var userTokenResponse = new UserTokenResponse
            {
                UserResponse = rToken.User,
                ExpirationToken = rToken.ExpireDate,
                Token = claimsIdentity.ToString()
            };


            return userTokenResponse;
        }


        private (string token, DateTime expiration) GenerateToken(User user)
        {
            var securityKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKeys, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(1);


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Credentials.username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Role,user.Rol),
                new Claim(ClaimTypes.Expiration,expiration.ToString("o")),
                new Claim("DNI",user.DNI),
                new Claim("IsActive",user.IsActive.ToString())
            };
            var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: expiration,
                    signingCredentials: credentials
                    );
            return (new JwtSecurityTokenHandler().WriteToken(token),expiration);
        }
    }
}
