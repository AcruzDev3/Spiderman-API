using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<List<User>?> GetAllNeighbours();
        Task<List<User>?> GetAllHeroes();
        Task<List<User>?> GetHeroesByIds(List<int> ids);
        Task<List<Crime>?> GetHeroCrimes(User model);
        Task<User> Add(User model);
        Task<User> Update(User model);
        Task Delete(User model);
        Task<bool> Exists(string email);
    }
}
