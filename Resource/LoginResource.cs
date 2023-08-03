﻿using Microsoft.IdentityModel.Tokens;
using SistemaEncomienda.Contracts.Response;
using SistemaEncomienda.Domain;
using SistemaEncomienda.Repository.Abstract;
using SistemaEncomienda.Resource.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaEncomienda.Resource
{
    public class LoginResource : ILoginResource
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        public LoginResource(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public UserTokenResponse GetUserLogin(UserLogin userLogin)
        {
            var currentUser = _loginRepository.GetUser(userLogin).Result;

            if (currentUser != null)
            {
                var token = GenerateToken(currentUser);
                var userTokenResponse = new UserTokenResponse
                {
                    UserResponse = new UserResponse
                    {
                        UserId = currentUser.UserId,
                        Rol = currentUser.Rol,
                        Name = currentUser.Name,
                        LastName = currentUser.LastName,
                        Email = currentUser.Email,
                        DNI = currentUser.DNI,
                        Telephone = currentUser.Telephone,
                        Status = currentUser.Status
                    },
                    Token = token
                };

                return userTokenResponse;
            }
            return null;
        }
        
        private string GenerateToken(User user)
        {
            var securityKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKeys, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Credentials.username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Role,user.Rol),
                new Claim("DNI",user.DNI),
                new Claim("Status",user.Status.ToString())
            };
            var token = new JwtSecurityToken(
                                    _configuration["Jwt:Issuer"],
                                    _configuration["Jwt:Audience"],
                                    claims,
                                    expires: DateTime.Now.AddMinutes(15),
                                    signingCredentials: credentials
                                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}