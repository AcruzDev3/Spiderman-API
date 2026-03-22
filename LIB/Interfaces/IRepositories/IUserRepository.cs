using LIB.Models;
using LIB.ViewModels;

namespace LIB.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id); 
        Task<List<User>?> GetByIds(List<int> ids);
        Task<List<User>> GetAll();
        Task<List<Crime>?> GetCrimes(User model);
        Task<User?> Exists(UserViewModel viewModel);
        Task Add(User model);
        void Update(User model);
        void Delete(User model);
        Task<int> SaveChanges();
        Task<Role?> GetRoleAsync(string? role);
    }
}
