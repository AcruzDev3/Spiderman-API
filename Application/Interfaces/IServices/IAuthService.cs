using Application.Contracts.Requests.Auth;
using Application.Contracts.Responses.Auth;

namespace Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegisterResponse> Register(RegisterRequest request);
    }
}
