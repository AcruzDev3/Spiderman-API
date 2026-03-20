using API.DTOs;
using LIB.DTOs;
using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class UserManager : IManager<UserViewModel, CreateUserRequest, User>
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        public UserManager(SpidermanContext context, CrimeManager crimeManager)
        {
            this._context = context;
            this._crimeManager = crimeManager;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            UserViewModel? viewModel = null;
            try {
                if (id < 0) throw new Exception("El id del usuario no es válido");

                User? model = await this._context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (model == null) throw new Exception("El usuario no existe");

                viewModel = new UserViewModel(model);
            } catch(Exception) {
                throw;
            }
            return viewModel;
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            List<UserViewModel> viewModels = new List<UserViewModel>();
            try {
                List<User>? models = await this.GetAllModels();
                
                if(models == null) throw new Exception("No se han podido obtener los usuarios");
                
                foreach (User model in models) viewModels.Add(new UserViewModel(model));
            }
            catch (Exception) {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateUserRequest dto)
        {
            try
            {
                Role? role = await this.VerifyRoleUser(dto.Role);
                
                if (role == null) throw new Exception("El rol del usuario no existe");

                UserViewModel viewModel = new UserViewModel(dto);

                if(await Exists(new UserViewModel(dto)) == null) throw new Exception("El usuario ya existe");


                User? model = new User(viewModel, role, dto.Password);
                if (model == null) throw new Exception("El usuario no es válido");
               
                await this._context.Users.AddAsync(model);
                
                int rowAffected = await this._context.SaveChangesAsync();
                if (rowAffected != 1) throw new Exception("No se pudo crear el usuario");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                if(id < 1) throw new Exception("El usuario no es válido");

                User? model = await this.GetModel(id);
                if(model == null) throw new Exception("El usuario no existe");

                // Delete the crimes associated with the user
                int rowsAffectedDeletedCrimes = await this._crimeManager.DeleteAllCrimesAssociatedWhithId(model.UserId);
                if(rowsAffectedDeletedCrimes == -1) throw new Exception("No se pudieron eliminar los crímenes asociados al usuario");

                this._context.Users.Remove(model);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se pudo eliminar el usuario");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> Exists(UserViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("La vista modelo del usuario es nula");

                return await this._context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(
                        u => u.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        u.Email.Equals(viewModel.Email, StringComparison.CurrentCultureIgnoreCase) &&
                        u.Role.Name.Equals(viewModel.Role, StringComparison.CurrentCultureIgnoreCase)
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Role?> VerifyRoleUser(string roleName)
        {
            try {
                return await this._context.Roles.FirstOrDefaultAsync(
                    r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)
                );
            } catch (Exception) {
                throw;
            }
        }

        private async Task<User?> GetModel(int id)
        {
            return await this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
        }

        private async Task<List<User>?> GetAllModels()
        {
            return await this._context.Users.AsNoTracking().ToListAsync();
        }
        
    }
}
