using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Services;
using UserAPI.Domain.Security;

namespace UserAPI.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtTokenDto GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); 

            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtTokenDto()
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
       
    }
}
