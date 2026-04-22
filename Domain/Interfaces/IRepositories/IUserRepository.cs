using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id); 
        Task<List<User>?> GetAll();
        Task<List<User>?> GetByIds(List<int> ids);
        Task<List<Crime>?> GetCrimes(User model);
        Task<User> Add(User model);
        Task<User> Update(User model);
        Task Delete(User model);
        Task<bool> Exists(string email);
    }
}
