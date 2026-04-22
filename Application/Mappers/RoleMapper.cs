using Application.Contracts.Requests.Role;
using Application.Contracts.Responses;
using Domain.Models;

namespace Application.Mappers
{
    public static class RoleMapper
    {
        public static RoleResponse ToResponse(Role role) {
            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToModel(CreateRoleRequest request) {
            return new Role(request.Name);
        }
    }
}
