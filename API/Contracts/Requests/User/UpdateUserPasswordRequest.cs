using LIB.Validation;
using System.ComponentModel.DataAnnotations;

namespace API.Contracts.Requests.User
{
    public class UpdateUserPasswordRequest
    {
        [Required(ErrorMessage = "La contraseña anterior es obligatoria")]
        [StrongPasswrod]
        public string previousPassword { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [StrongPasswrod]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "La confirmación de la nueva contraseña es obligatoria")]
        [StrongPasswrod]
        public string newPasswordConfirmation { get; set; }
    }
}
