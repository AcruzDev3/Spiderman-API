using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.User
{
    public class ChangeEmailRequest
    {
        [Required(ErrorMessage = "El id del usuario es obligatorio")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [StringLength(255, ErrorMessage = "La longitud del correo electrónico no pueder ser mayor a 255 caracteres")]
        public string Email { get; set; } = null!;
    }
}
