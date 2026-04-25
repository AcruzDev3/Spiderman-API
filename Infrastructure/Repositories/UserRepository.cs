using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SpidermanContext _context;
        public UserRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<User?> GetById(int id) {
            try {
                UserEntity? entity = await this._context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);

                if (entity == null) return null;

                return await this.GetUserModel(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el usuario con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<User>?> GetByIds(List<int> ids) {
            try {
                List<UserEntity> entities = await this._context.Users
                .Where(u => ids.Contains(u.UserId))
                .ToListAsync();
                List<User> users = new List<User>();
                foreach (UserEntity entity in entities) users.Add(await this.GetUserModel(entity));
                return users;
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener los usuarios con ids {string.Join(", ", ids)} de la base de datos: {ex.Message}");
            }
        }

        public async Task<User?> GetByEmail(string email) {
            try {
                UserEntity? entity = await this._context.Users
                    .Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (entity == null) return null;

                return await this.GetUserModel(entity);
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener el usuario con email {email} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<User>?> GetAll() {
            try {
                List<UserEntity> entities = await this._context.Users
                                    .ToListAsync();
                if (entities == null || entities.Count == 0) return null;
                List<User> users = new List<User>();
                foreach (UserEntity entity in entities) users.Add(await this.GetUserModel(entity));
                return users;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los usuarios de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<Crime>?> GetCrimes(User model) {
            try {
                List<CrimeEntity> crimesEntities = await _context.Crimes
                    .Where(c => c.Heroes.Any(u => u.UserId == model.Id))
                    .ToListAsync();
                List<Crime> crimes = new List<Crime>();
                return crimes;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener los crímenes del usuario con id {model.Id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<User> Add(User model) {
            try {
                UserEntity entity = UserMapper.ToEntity(model);
                await this._context.Users.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el usuario a la base de datos");
                return UserMapper.ToDomain(entity, RoleMapper.ToDomain(entity.Role));
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el usuario a la base de datos: {ex.Message}");
            }
        }

        public async Task<User> Update(User model) {
            try {
                UserEntity entity = UserMapper.ToEntity(model);
                this._context.Users.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el usuario a la base de datos");
                return UserMapper.ToDomain(entity, RoleMapper.ToDomain(entity.Role));
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al actualizar el usuario con id {model.Id} en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(User model) {
            try {
                this._context.Users.Remove(UserMapper.ToEntity(model));
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el usuario a la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el usuario con id {model.Id} de la base de datos: {ex.Message}");
            }
            
        }

        public async Task<bool> Exists(string email) {
            try {
                UserEntity? entity = this._context.Users
               .AsNoTracking()
               .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (entity == null) return false;
                else return true;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al verificar la existencia del usuario con email {email} en la base de datos: {ex.Message}");
            }
           
        }

        private async Task<User> GetUserModel(UserEntity entity) {
            try {
                Role role = RoleMapper.ToDomain(entity.Role);
                return UserMapper.ToDomain(entity, role);
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al mapear el usuario con id {entity.UserId} de la base de datos: {ex.Message}");
            }
        }
    }
}
