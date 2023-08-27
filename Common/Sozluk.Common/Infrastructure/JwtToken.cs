using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
namespace Sozluk.Common.Infrastructure
{
	public  class JwtToken
	{
        private static readonly IConfiguration configuration;

        public static string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(5);
            var token = new JwtSecurityToken( claims:claims,
                expires:expiry,
                signingCredentials:creds,
                notBefore:DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

