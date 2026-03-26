using Application.Interfaces.IRepositories;
using Infrastructure.EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SpidermanContext _context;

        public UserRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<UserEntity?> GetById(int id) {
            return await this._context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<List<UserEntity>?> GetByIds(List<int> ids) {
            return await this._context.Users
                .Where(u => ids.Contains(u.UserId))
                .ToListAsync();
        }

        public async Task<List<UserEntity>> GetAll() {
            return await this._context.Users
                .ToListAsync();
        }

        public async Task<List<CrimeEntity>?> GetCrimes(UserEntity model) {
            return await _context.Crimes
                .Where(c => c.Heroes.Any(u => u.UserId == model.UserId))
                .ToListAsync();
        }
        public async Task Add(UserEntity model) {
            await this._context.Users.AddAsync(model);
        }

        public void Update(UserEntity model) {
            this._context.Users.Update(model);
        }

        public void Delete(UserEntity model) {
            this._context.Users.Remove(model);
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }

        public async Task<UserEntity?> Exists(UserViewModel viewModel) {
            return this._context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email.Equals(viewModel.Email) &&
                u.Name.Equals(viewModel.Name) && u.Role.Name.Equals(viewModel.Role));
        }

        public async Task<RoleEntity?> GetRoleAsync(string? role) {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase));
        }
    }
}
