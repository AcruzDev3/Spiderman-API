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
            UserEntity? entity = await this._context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (entity == null) return null;

            return await this.GetUserModel(entity);
        }

        public async Task<List<User>?> GetByIds(List<int> ids) {
            List<UserEntity> entities = await this._context.Users
                .Where(u => ids.Contains(u.UserId))
                .ToListAsync();
            List<User> users = new List<User>();
            foreach (UserEntity entity in entities) users.Add(await this.GetUserModel(entity));
            return users;
        }

        public async Task<List<User>> GetAll() {
            List<UserEntity> entities = await this._context.Users
                                    .ToListAsync();
            List<User> users = new List<User>();
            foreach (UserEntity entity in entities) users.Add(await this.GetUserModel(entity));
            return users;
        }

        public async Task<List<Crime>?> GetCrimes(User model) {
            List<CrimeEntity> crimesEntities = await _context.Crimes
                .Where(c => c.Heroes.Any(u => u.UserId == model.Id))
                .ToListAsync();
            List<Crime> crimes = new List<Crime>();

            foreach (CrimeEntity entity in crimesEntities) {
                List<Criminal> criminals = GetCriminalsOfCrime(entity.Criminals.ToList());
                List<User> heroes = GetHeroesOfCrime(entity.Heroes.ToList());
                Address addres = AddressMapper.ToDomain(entity.Address);
                CrimeGrade grade = CrimeGradeMapper.ToDomain(entity.Grade);
                CrimeType type = CrimeTypeMapper.ToDomain(entity.Type);
                
                crimes.Add(CrimeMapper.ToDomain(entity, heroes, criminals, addres, grade, type));
            }

            return crimes;
        }

        public async Task Add(User model) {
            await this._context.Users.AddAsync(UserMapper.ToEntity(model));
            int rowsAffected = await this._context.SaveChangesAsync();
            if(rowsAffected != 1) throw new InfrastructureException("Error al añadir el usuario a la base de datos");
        }

        public async Task Update(User model) {
            this._context.Users.Update(UserMapper.ToEntity(model));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el usuario a la base de datos");
        }

        public async Task Delete(User model) {
            this._context.Users.Remove(UserMapper.ToEntity(model));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el usuario a la base de datos");
        }

        public async Task<bool> Exists(string email) {
            UserEntity? entity = this._context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (entity == null) return false;
            else return true;
        }

        public async Task<Role?> GetRoleAsync(int idRole) {
            RoleEntity? entity = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoleId == idRole);

            if (entity == null) return null;
            else return RoleMapper.ToDomain(entity);
        }

        private List<Criminal> GetCriminalsOfCrime(List<CriminalEntity> criminalsEntities) {
            List<Criminal> criminals = new List<Criminal>();

            foreach (CriminalEntity criminal in criminalsEntities) {
                CriminalRiskLevel risk = CriminalRiskLevelMapper.ToDomain(criminal.Risk);
                criminals.Add(CriminalMapper.ToDomain(criminal, risk));
            }
            return criminals;
        }

        private List<User> GetHeroesOfCrime(List<UserEntity> usersEntities) {
            List<User> users = new List<User>();

            foreach (UserEntity user in usersEntities) {
                Role role = RoleMapper.ToDomain(user.Role);
                users.Add(UserMapper.ToDomain(user, role));
            }
            return users;
        }

        private async Task<User> GetUserModel(UserEntity entity) {
            Role role = RoleMapper.ToDomain(entity.Role);
            return UserMapper.ToDomain(entity, role);
        }
    }
}
