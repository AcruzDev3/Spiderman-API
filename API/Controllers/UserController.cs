using Application;
using Application.Constans;
using Application.Contracts.Requests.User;
using Application.Contracts.Responses;
using Application.Enums;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAzureImageService _azureImageService;
        public UserController(IUserService userService, IAzureImageService azureService) {
            this._userService = userService;
            this._azureImageService = azureService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            UserResponse viewModel = await this._userService.GetById(id);
            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            List<UserResponse> viewModels = await this._userService.GetAll();
            return Ok(viewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request) {
            UserResponse user = null;

            if(request.Image != null) {
                string urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Users.ToString().ToLower(),
                    request.Image.ContentType
                );
                user = await this._userService.Create(request, urlImage);
            } else {
                user = await this._userService.Create(request, DefaultImagesPath.User);
            }

            return CreatedAtAction(nameof(GetById), user);
        }
    }
}
