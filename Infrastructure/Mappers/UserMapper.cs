using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(UserEntity entity, Role role) {
            return new User(entity.UserId, entity.Name, entity.Email, entity.Password, role, entity.Image);
        }

        public static UserEntity ToEntity(User user) {
            return new UserEntity {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.Role.Id,
                Image = user.Image
            };
        }
    }
}
