using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.User
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El rol del usuario es obligatorio")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [StringLength(300, ErrorMessage = "La ruta de la imagen no puede superar ")]
        public IFormFile? Image { get; set; }
    }
}
