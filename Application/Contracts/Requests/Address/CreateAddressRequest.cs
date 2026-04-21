using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Address
{
    public class CreateAddressRequest
    {
        [Required(ErrorMessage = "El número es obligatorio")]
        public int Number { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public SideType Side { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [StringLength(5, ErrorMessage = "La longitud del codigo postal no puede ser mayor a 5 caracteres")]
        [MinLength(5, ErrorMessage = "La longitud del codigo postal no puede ser menor a 5 caracteres")]
        public string ZipCode { get; set; } = null!;

        [Required(ErrorMessage = "La calle es obligatoria")]
        [StringLength(150, ErrorMessage = "La longitud de la calle no puede ser mayor a 150 caracteres")]
        public string Street { get; set; } = null!;

        public IFormFile? Image { get; set; }
    }
}
