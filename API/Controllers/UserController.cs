using Application.Contracts.Requests.User;
using Application.Contracts.Responses;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            this._userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._userService.GetById(id));
        

        [HttpGet]
        public async Task<IActionResult> Index()
            => Ok(await this._userService.GetAll());
        

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request) {
            UserResponse userResponse = await this._userService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = userResponse.Id}, userResponse);
        }
            
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest request)
            => Ok(await this._userService.Update(request));

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request) {
            await this._userService.ChangePassword(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await this._userService.Delete(id);
            return NoContent();
        }
    }
}
