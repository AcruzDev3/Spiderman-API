using Application.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.User
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "El nombre del usuario el obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [StringLength(255, ErrorMessage = "La longitud del correo electrónico no pueder ser mayor a 255 caracteres")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StrongPasswrod]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El rol del usuario es obligatorio")]
        public int RoleId { get; set; }

        [StringLength(300, ErrorMessage = "La ruta de la imagen no puede superar ")]
        public IFormFile? Image { get; set; }
    }
}
