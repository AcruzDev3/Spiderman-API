using LIB.DTOs.User;
using LIB.ViewModels;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetById(int id);
        Task<List<UserViewModel>> GetAll();
        Task Create(CreateUserRequest dto);
        Task Update(UpdateUserRequest dto);
        Task Delete(int id);
    }
}
