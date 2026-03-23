using LIB.DTOs.User;
using LIB.Models;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string? Image { get; set; }

        public UserViewModel(User model) {
            try {
                if (model == null) throw new Exception("El usuario no puede ser nulo");

                this.Id = model.UserId;
                this.Name = model.Name;
                this.Email = model.Email;
                this.Role = model.Role.Name;
                this.Image = model.Image;
            } catch (Exception) {
                throw;
            }
        }

        public UserViewModel(CreateUserRequest dto) {
            try {
                if (dto == null) throw new Exception("El DTO del usuario no puede ser nulo");

                this.Name = dto.Name;
                this.Email = dto.Email;
                this.Role = dto.Role;
                this.Image = dto.Image;
            } catch (Exception) {
                throw;
            }
        }

        public UserViewModel(UpdateUserRequest dto) {
            try {
                if (dto == null) throw new Exception("El DTO del usuario no puede ser nulo");
                this.Id = dto.Id;
                this.Name = dto.Name;
                this.Email = dto.Email;
                this.Role = dto.Role;
                this.Image = dto.Image;

            } catch (Exception) {
                throw;
            }
        }
    }
}
