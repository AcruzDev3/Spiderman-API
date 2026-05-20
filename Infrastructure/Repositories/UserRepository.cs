using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Infrastructure.Persistence.Entities;
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
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

                if (entity == null) return null;

                return await this.GetUserModel(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el usuario con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<User>?> GetHeroesByIds(List<int> ids) {
            try {
                List<UserEntity> entities = await this._context.Users
                .Where(u => ids.Contains(u.UserId))
                .Include(u=> u.Role)
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
                    .Where(u => u.Email == email)  
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync();

                if (entity == null) return null;

                return await this.GetUserModel(entity);
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener el usuario con email {email} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<User>?> GetAllNeighbours() {
            try {
                List<UserEntity> entities = await this._context.Users
                                    .Where(u => u.Role.Name == "NEIGHBOUR")
                                    .Include(u => u.Role)
                                    .ToListAsync();
                if (entities == null || entities.Count == 0) return null;
                List<User> users = new List<User>();
                foreach (UserEntity entity in entities) users.Add(await this.GetUserModel(entity));
                return users;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los usuarios de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<User>?> GetAllHeroes() {
            try {
                List<UserEntity> entities = await this._context.Users
                                    .Where(u => u.Role.Name == "HERO")
                                    .Include(u => u.Role)
                                    .ToListAsync(); 
                if (entities == null || entities.Count == 0) return null;
                List<User> users = new List<User>();
                foreach (UserEntity entity in entities) users.Add(await this.GetUserModel(entity));
                return users;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los usuarios de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<Crime>?> GetHeroCrimes(User model) {
            try {
                List<CrimeEntity> crimesEntities = await _context.Crimes
                    .Where(c => c.Heroes.Any(u => u.UserId == model.Id))
                    .Include(c => c.Grade)
                    .Include(c => c.Type)
                    .Include(c => c.Address)
                    .Include(c => c.Criminals)
                    .Include(c => c.Heroes)
                    .ToListAsync();
                List<Crime> crimes = new List<Crime>();
                foreach(CrimeEntity crimeEntity in crimesEntities) crimes.Add(await this.GetCrimeModel(crimeEntity));
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
               .FirstOrDefault(u => u.Email == email);
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

        private async Task<Crime> GetCrimeModel(CrimeEntity entity) {
            try {
                CrimeGrade grade = CrimeGradeMapper.ToDomain(entity.Grade);
                CrimeType type = CrimeTypeMapper.ToDomain(entity.Type);
                Address address = AddressMapper.ToDomain(entity.Address);

                List<User> users = new List<User>();
                foreach (UserEntity userEntity in entity.Heroes) {
                    Role role = RoleMapper.ToDomain(userEntity.Role);
                    users.Add(UserMapper.ToDomain(userEntity, role));
                }

                List<Criminal> criminals = new List<Criminal>();
                foreach (CriminalEntity criminalEntity in entity.Criminals) {
                    CriminalRiskLevel risk = CriminalRiskLevelMapper.ToDomain(criminalEntity.Risk);
                    criminals.Add(CriminalMapper.ToDomain(criminalEntity, risk));
                }

                return CrimeMapper.ToDomain(entity, users, criminals, address, grade, type);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el modelo del crimen de la base de datos: {ex.Message}");
            }
        }
    }
}
