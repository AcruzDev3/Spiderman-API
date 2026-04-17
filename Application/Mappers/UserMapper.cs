using Domain.Models;
using Application.Contracts.Responses;

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
    }
}
