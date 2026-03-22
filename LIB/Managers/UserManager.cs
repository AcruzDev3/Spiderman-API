using LIB.DTOs.User;
using LIB.Interfaces.IManagers;
using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;

namespace LIB.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly ICrimeRepository _crimeRepository;
        public UserManager(IUserRepository userRepository, ICrimeRepository crimeRepository) {
            this._userRepository = userRepository;
            this._crimeRepository = crimeRepository;
        }

        public async Task<UserViewModel> GetById(int id) {
            UserViewModel? viewModel = null;
            try {
                User? model = await this._userRepository.GetById(id);
                if (model == null) throw new Exception("El usuario no existe");

                viewModel = new UserViewModel(model);
            } catch (Exception) {
                throw;
            }
            return viewModel;
        }

        public async Task<List<UserViewModel>> GetAll() {
            List<UserViewModel> viewModels = new List<UserViewModel>();
            try {
                List<User>? models = await this._userRepository.GetAll();
                if (models == null) throw new Exception("No se han podido obtener los usuarios");

                foreach (User model in models) viewModels.Add(new UserViewModel(model));

            } catch (Exception) {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateUserRequest dto) {
            try {
                Role? role = await this._userRepository.GetRoleAsync(dto.Role);
                if (role == null) throw new Exception("El rol del usuario no existe");

                UserViewModel viewModel = new UserViewModel(dto);
                if (await this._userRepository.Exists(new UserViewModel(dto)) != null) throw new Exception("El usuario ya existe");

                User model = new User(dto, role);
                if (model == null) throw new Exception("El usuario no es válido");

                await this._userRepository.Add(model);

                int rowsAffected = await this._userRepository.SaveChanges();
                if (rowsAffected != 1) throw new Exception("No se pudo crear el usuario");
            } catch (Exception) {
                throw;
            }
        }

        public async Task Update(UpdateUserRequest dto) {
            try {
                if(dto == null) throw new Exception("El usuario no es válido");

                User? user = await this._userRepository.GetById(dto.Id);
                if(user == null) throw new Exception("El usuario no existe");

                Role? role = await this._userRepository.GetRoleAsync(dto.Role);
                if(role == null) throw new Exception("El rol del usuario no existe");

                UserViewModel viewModel = new UserViewModel(dto);

                User newUser = new User(viewModel, role);

                this._userRepository.Update(newUser);
                await this._userRepository.SaveChanges();
            } catch(Exception) {
                throw;
            }
        }

        public async Task ChangePassword() {
            try {

            } catch (Exception) {
                throw;
            }
        }

        public async Task Delete(int id) {
            try {
                User? user = await this._userRepository.GetById(id);
                if (user == null) throw new Exception("El usuario no existe");

                List<Crime>? crimes = await this._userRepository.GetCrimes(user);
                
                this._userRepository.Delete(user);
                int rowsAffected = await this._userRepository.SaveChanges();

                if (crimes == null) return;

                List<Crime>? crimesWithoutHero = crimes
                    .Where(c => c.Heroes.Any())
                    .ToList();

                if (crimesWithoutHero.Any()) {
                    this._crimeRepository.DeleteRange(crimesWithoutHero);
                    await this._crimeRepository.SaveChanges();
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}
