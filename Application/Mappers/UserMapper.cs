using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.User;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(User user, RoleResponse role) {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Role = role,
                Image = user.Image
            };
        }

        public static User ToModel(CreateUserRequest request, Role role, string pathImage) {
            return new User(request.Name, request.Email, request.Password, role, pathImage);
        }
        public static User ToModel(UpdateUserRequest request, Role role, string password, string pathImage) {
            return new User(request.Name, request.Email, password, role, pathImage);
        }
    }
}
