using Application.Contracts.Requests.User;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task Create(CreateUserRequest request);
        Task Update(UpdateUserRequest request);
        Task Delete(int id);
    }
}
