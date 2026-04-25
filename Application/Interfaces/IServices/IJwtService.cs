using System.Security.Claims;

namespace Application.Interfaces.IServices
{
    public interface IJwtService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        DateTime GetAccessTokenExpiration();
    }
}
