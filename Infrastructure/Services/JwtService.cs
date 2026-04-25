using Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration config) {
        this._configuration = config;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims) {
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"])
        );

        SigningCredentials credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        DateTime expiration = GetAccessTokenExpiration();

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: this._configuration["Jwt:Issuer"],
            audience: this._configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string jwt = handler.WriteToken(token);

        return jwt;
    }

    public DateTime GetAccessTokenExpiration() {
        double minutes = Convert.ToDouble(this._configuration["Jwt:DurationInMinutes"]);
        DateTime expiration = DateTime.UtcNow.AddMinutes(minutes);
        return expiration;
    }
}
