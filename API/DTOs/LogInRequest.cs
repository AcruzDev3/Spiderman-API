using System.ComponentModel.DataAnnotations;

namespace API.DTOs {
    public class LogInRequest {

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [[EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [StringLength(255, ErrorMessage = "La longitud del correo electrónico no pueder ser mayor a 255 caracteres")]]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
