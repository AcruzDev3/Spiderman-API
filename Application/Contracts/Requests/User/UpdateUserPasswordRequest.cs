using Application.Validation;

using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.User
{
    public class UpdateUserPasswordRequest
    {
        [Required(ErrorMessage = "La contraseña anterior es obligatoria")]
        [StrongPasswrod]
        public string PreviousPassword { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [StrongPasswrod]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "La confirmación de la nueva contraseña es obligatoria")]
        [StrongPasswrod]
        public string NewPasswordConfirmation { get; set; }
    }
}
