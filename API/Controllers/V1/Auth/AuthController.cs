using Application.Contracts.Requests.Auth;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace API.Controllers.V1.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request) {
            
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request) {

        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenRequest request) {
            
        }
    }
}
