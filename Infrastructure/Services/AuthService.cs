using Application.Constans;
using Application.Contracts.Requests.Auth;
using Application.Contracts.Responses.Auth;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, 
            IPasswordHasher passwordHasher, IRoleRepository roleRepository) {
            this._userRepository = userRepository;
            this._jwtService = jwtService;
            this._passwordHasher = passwordHasher;
            this._roleRepository = roleRepository;  
        }

        public async Task<LoginResponse> Login(LoginRequest request) {
            User? user = await this._userRepository.GetByEmail(request.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            bool passwordValid = this._passwordHasher.Verify(request.Password, user.Password);

            if (!passwordValid) 
                throw new UnauthorizedAccessException("Invalid credentials.");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            string accessToken = this._jwtService.GenerateAccessToken(claims);
            DateTime expiration = this._jwtService.GetAccessTokenExpiration();



            LoginResponse response = new LoginResponse {
                Token = accessToken,
                Expiration = expiration
            };

            return response;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request) {
            User? model = await this._userRepository.GetByEmail(request.Email);
            
            if (model != null) {
                RegisterResponse conflictResponse = new RegisterResponse {
                    Success = false,
                    Message = "Email already in use."
                };
                return conflictResponse;
            }

            Role? neighbourRole = await this._roleRepository.GetNeighbourRole();
            if(neighbourRole == null) {
                RegisterResponse roleNotFoundResponse = new RegisterResponse {
                    Success = false,
                    Message = "Neighbour role not found."
                };
                return roleNotFoundResponse;
            }

            string passwordHash = this._passwordHasher.Hash(request.Password);

            User user = new User(request.Name, request.Email, passwordHash, neighbourRole, DefaultImagesPath.User);

            await this._userRepository.Add(user);

            RegisterResponse response = new RegisterResponse {
                Success = true,
                Message = "User registered successfully."
            };

            return response;
        }
    }
}
