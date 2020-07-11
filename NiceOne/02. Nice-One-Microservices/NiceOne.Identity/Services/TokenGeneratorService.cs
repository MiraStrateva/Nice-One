namespace NiceOne.Identity.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using NiceOne;
    using NiceOne.Identity.Data.Entities;

    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly ApplicationSettings applicationSettings;

        public TokenGeneratorService(IOptions<ApplicationSettings> applicationSettings) 
            => this.applicationSettings = applicationSettings.Value;

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("FirstName", user.FirstName),
                    new Claim(ClaimTypes.Role, "Administrator")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
