using LIB.DTOs.User;
using LIB.ViewModels;

namespace LIB.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Image { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();

    public User(UserViewModel viewModel, Role role, string password) {
        try {
            if(String.IsNullOrEmpty(password)) throw new Exception("La contraseña no es válido");
            if(viewModel == null) throw new Exception("El modelo vista no es válido");
            if(role == null) throw new Exception("El rol es válido");

            this.Name = viewModel.Name;
            this.Email = viewModel.Email;
            this.Password = password; // Aquí habría que hashear la contraseña
            this.RoleId = role.RoleId;
            this.Image = viewModel.Image;
            this.Role = role;
        } catch(Exception) {
            throw;
        }
    }

    public User(CreateUserRequest dto, Role role) {
        if(String.IsNullOrWhiteSpace(dto.Name)) throw new Exception("El nombre del usuario no es válido");
        if(String.IsNullOrWhiteSpace(dto.Email)) throw new Exception("El email del usuario no es válido");
        if(String.IsNullOrWhiteSpace(dto.Password)) throw new Exception("La contraseña del usuario no es válida");

        this.Name = dto.Name;
        this.Email = dto.Email;
        this.Password = dto.Password;
        this.RoleId = Role.RoleId;
        this.Image = dto.Image;
        this.Role = role;
    }

    public User(UserViewModel viewModel, Role role) {
        this.Name = viewModel.Name;
        this.Email = viewModel.Email;
        this.RoleId = role.RoleId;
        this.Role = role;
        this.Image = viewModel.Image;
    }
}
