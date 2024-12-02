using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(User user)
        {
            // Define claims including Name and Role (or other necessary claims)
            /*var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("role", user.Role) // Custom role claim name
            };*/
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // or a custom claim like "role"
            };


            // Secret key for signing the JWT (ensure this is the same as in your configuration)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Credentials for signing the token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create JWT token with claims and expiration time
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set to 1 hour or another suitable time
                signingCredentials: creds,
                notBefore: DateTime.UtcNow // Optionally set a "NotBefore" claim
            );

            // Return the generated token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
