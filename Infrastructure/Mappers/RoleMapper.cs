using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class RoleMapper
    {
        public static Role ToDomain(RoleEntity entity) {
            return new Role(entity.RoleId, entity.Name);
        }

        public static RoleEntity ToEntity(Role role) {
            return new RoleEntity {
                RoleId = role.Id,
                Name = role.Name
            };
        }
    }
}
