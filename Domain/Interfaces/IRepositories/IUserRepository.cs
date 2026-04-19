using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id); 
        Task<List<User>?> GetByIds(List<int> ids);
        Task<List<User>> GetAll();
        Task<List<Crime>?> GetCrimes(User model);
        Task Add(User model);
        Task Update(User model);
        Task Delete(User model);
        Task<Role?> GetRoleAsync(int idRole);
        Task<bool> Exists(string email);
    }
}
