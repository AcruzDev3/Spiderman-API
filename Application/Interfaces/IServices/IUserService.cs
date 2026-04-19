using Application.Contracts.Requests.User;
using Application.Contracts.Responses;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetById(int id);
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> Create(CreateUserRequest request, string pathImageProfile);
        Task<UserResponse> Update(UpdateUserRequest request, string pathImageProfile);
        Task Delete(int id);
    }
}
