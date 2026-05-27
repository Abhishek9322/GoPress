using GoPress.Application.Interfaces.Services;
using GoPress.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GoPress.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim(ClaimTypes.Name, user.FullName),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Role, user.Role.ToString())
           };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]));

            var creds = new SigningCredentials(
              key,
              SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.UtcNow.AddMinutes(
              Convert.ToDouble(
                  _configuration["Jwt:DurationInMinutes"]));

            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Audience"],
              claims: claims,
              expires: expiry,
              signingCredentials: creds
          );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
