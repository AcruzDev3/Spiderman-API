using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
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
            Role role = RoleMapper.ToDomain(entity.Role);
            return UserMapper.ToDomain(entity, role); 
        }

        public async Task<List<User>?> GetByIds(List<int> ids) {
            List<UserEntity> entities = await this._context.Users
                .Where(u => ids.Contains(u.UserId))
                .ToListAsync();
            List<User> users = new List<User>();
            foreach (UserEntity entity in entities) {
                Role role = RoleMapper.ToDomain(entity.Role);
                users.Add(UserMapper.ToDomain(entity, role));
            }
            return users;
        }

        public async Task<List<User>> GetAll() {
            List<UserEntity> entities = await this._context.Users
                                    .ToListAsync();
            List<User> users = new List<User>();
            foreach (UserEntity entity in entities) {
                Role role = RoleMapper.ToDomain(entity.Role);
                users.Add(UserMapper.ToDomain(entity, role));
            }
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
        }

        public void Update(User model) {
            this._context.Users.Update(UserMapper.ToEntity(model));
        }

        public void Delete(User model) {
            this._context.Users.Remove(UserMapper.ToEntity(model));
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }

        public async Task<User?> Exists(User model) {
            UserEntity? entity = this._context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email.Equals(model.Email) &&
                u.Name.Equals(model.Name) && u.Role.Name.Equals(model.Role));
            Role? role = await GetRoleAsync(model.Role.Name);
            return UserMapper.ToDomain(entity, role);
        }

        public async Task<Role?> GetRoleAsync(string? role) {
            RoleEntity? entity = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase));
            return RoleMapper.ToDomain(entity);
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
    }
}
