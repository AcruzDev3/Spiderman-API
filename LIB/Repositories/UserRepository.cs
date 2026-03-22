using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SpidermanContext _context;

        public UserRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<User?> GetById(int id) {
            return await this._context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<List<User>?> GetByIds(List<int> ids) {
            return await this._context.Users
                .Where(u => ids.Contains(u.UserId))
                .ToListAsync();
        }

        public async Task<List<User>> GetAll() {
            return await this._context.Users
                .ToListAsync();
        }

        public async Task<List<Crime>?> GetCrimes(User model) {
            return await _context.Crimes
                .Where(c => c.Heroes.Any(u => u.UserId == model.UserId))
                .ToListAsync();
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

        public async Task<User?> Exists(UserViewModel viewModel) {
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
