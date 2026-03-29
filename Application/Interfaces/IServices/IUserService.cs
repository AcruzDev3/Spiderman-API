using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task Create(User dto);
        Task Update(User dto);
        Task Delete(int id);
    }
}
