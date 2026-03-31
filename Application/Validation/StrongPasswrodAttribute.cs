using System.ComponentModel.DataAnnotations;

namespace Application.Validation
{
    public class StrongPasswrodAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext) {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("La contraseña es obligatoria.");

            if (password.Length < 8)
                return new ValidationResult("La contraseña debe tener al menos 8 caracteres.");

            if (!password.Any(char.IsUpper))
                return new ValidationResult("Debe contener al menos una letra mayúscula.");

            if (!password.Any(char.IsLower))
                return new ValidationResult("Debe contener al menos una letra minúscula.");

            if (!password.Any(char.IsDigit))
                return new ValidationResult("Debe contener al menos un número.");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return new ValidationResult("Debe contener al menos un símbolo.");

            if (password.Contains(" "))
                return new ValidationResult("La contraseña no puede contener espacios.");

            return ValidationResult.Success;
        }

    }
}
