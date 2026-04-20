using Application.Constans;
using Application.Contracts.Requests.User;
using Application.Contracts.Responses;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICrimeRepository _crimeRepository;
        private readonly IAzureImageService _azureImageService;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepository, ICrimeRepository crimeRepository,
            IAzureImageService azureImageService, IPasswordHasher passwordHasher) {
            this._userRepository = userRepository;
            this._crimeRepository = crimeRepository;
            this._azureImageService = azureImageService;
            this._passwordHasher = passwordHasher;
        }

        public async Task<UserResponse> GetById(int id) {
            User? model = await this._userRepository.GetById(id);
            if (model == null) throw new NotFoundException("El usuario no existe");

            RoleResponse roleViewModel = RoleMapper.ToResponse(model.Role);
            return UserMapper.ToResponse(model, roleViewModel);
        }

        public async Task<List<UserResponse>> GetAll() {
            List<UserResponse> viewModels = new List<UserResponse>();
            
            List<User>? models = await this._userRepository.GetAll();
            if (models == null) throw new NotFoundException("No se han podido obtener los usuarios");

            foreach (User model in models) {
                RoleResponse roleViewModel = RoleMapper.ToResponse(model.Role);
                viewModels.Add(UserMapper.ToResponse(model, roleViewModel));
            }
            return viewModels;
        }

        public async Task<UserResponse> Create(CreateUserRequest request) {
            Role? role = await this._userRepository.GetRoleAsync(request.RoleId);
            if (role == null) throw new NotFoundException("El rol del usuario no existe");

            string urlImage = DefaultImagesPath.User;
            if (request.Image != null) {
                urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Users.ToString().ToLower(),
                    request.Image.ContentType
                );
            }

            request.Password = this._passwordHasher.Hash(request.Password);
            User model = UserMapper.ToModel(request, role, urlImage);

            if (await this._userRepository.Exists(model.Email)) 
                throw new Exception("El correo electrónico ya está registrado");

            return UserMapper.ToResponse(
                await this._userRepository.Add(model),
                RoleMapper.ToResponse(role)
            );
        }

        public async Task<UserResponse> Update(UpdateUserRequest request) {
            Role? role = await this._userRepository.GetRoleAsync(request.RoleId);
            if (role == null) throw new NotFoundException("El rol del usuario no existe");

            User? user = await this._userRepository.GetById(request.Id);
            if(user == null) throw new NotFoundException("El usuario no existe");

            string urlImage = user.Image;
            if (request.Image != null) {
                urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Users.ToString().ToLower(),
                    request.Image.ContentType
                );
            }

            User newUser = UserMapper.ToModel(request, role, user.Password, urlImage);

            return UserMapper.ToResponse(
                await this._userRepository.Update(newUser),
                RoleMapper.ToResponse(role)
            );
        }


        // TODO
        public async Task ChangePassword(ChangePasswordRequest request) {
            User? user = await this._userRepository.GetById(request.UserId);
            if(user == null) throw new NotFoundException("El usuario no existe");

            if(!request.NewPassword.Equals(request.ConfirmPassword)) 
                throw new Exception("Las contraseñas no coinciden");

            user.ChangePassword(this._passwordHasher.Hash(request.NewPassword)); 
            await this._userRepository.Update(user);
        }

        public async Task Delete(int id) {
            User? user = await this._userRepository.GetById(id);
            if (user == null) throw new NotFoundException("El usuario no existe");

            List<Crime>? crimes = await this._userRepository.GetCrimes(user);
                
            await this._userRepository.Delete(user);

            if (crimes == null) return;

            List<Crime>? crimesWithoutHero = crimes
                .Where(c => c.Users.Any())
                .ToList();

            if (crimesWithoutHero.Any()) await this._crimeRepository.DeleteRange(crimesWithoutHero);
        }
    }
}
