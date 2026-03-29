using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.ComponentModel;

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
                Address addres = GetAddressOfCrime(entity.Address);
                Crime crime = new Crime();
                crime.
                Crime crime = crimes.Add(CrimeMapper.ToDomain(entity, addres, criminals, heroes));
            }
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

        private Address GetAddressOfCrime(AddressEntity addressEntity) {
            return AddressMapper.ToDomain(addressEntity);
        }   

        public async Task Add(User model) {
            await this._context.Users.AddAsync(model);
        }

        public void Update(User model) {
            this._context.Users.Update(model);
        }

        public void Delete(User model) {
            this._context.Users.Remove(model);
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }

        public async Task<UserEntity?> Exists(User model) {
            return this._context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email.Equals(viewModel.Email) &&
                u.Name.Equals(viewModel.Name) && u.Role.Name.Equals(viewModel.Role));
        }

        public async Task<Role?> GetRoleAsync(string? role) {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase));
        }
    }
}
