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

        public static User ToModel(CreateUserRequest request, Role role, string hashedPassword, string pathImage) {
            return new User(request.Name, request.Email, hashedPassword, role, pathImage);
        }
        public static User ToModel(UpdateUserRequest request, string email, Role role, string password, string pathImage) {
            return new User(request.Name, email, password, role, pathImage);
        }
    }
}
