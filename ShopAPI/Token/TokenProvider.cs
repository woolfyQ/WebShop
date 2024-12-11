using Core.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopAPI.Token
{
    public sealed class TokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(UserDTO userDTO)
        {
            string secretKey = _configuration["JWT:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userDTO.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userDTO.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:ExpirationInMinutes")),
                SigningCredentials = credential,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
