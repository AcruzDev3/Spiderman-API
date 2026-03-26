using System.ComponentModel.DataAnnotations;

namespace API.Contracts.Requests.User
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [StringLength(255, ErrorMessage = "La longitud del correo electrónico no pueder ser mayor a 255 caracteres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El rol del usuario es obligatorio")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [StringLength(300, ErrorMessage = "La ruta de la imagen no puede superar ")]
        public IFormFile? Image { get; set; }
    }
}
