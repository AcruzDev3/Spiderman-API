using Application.Contracts.Requests.User;
using Application.Contracts.Responses;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetById(int id);
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> Create(CreateUserRequest request);
        Task<UserResponse> Update(UpdateUserRequest request);
        Task ChangePassword(ChangePasswordRequest request);
        Task ChangeEmail(ChangeEmailRequest request);
        Task Delete(int id);
    }
}
