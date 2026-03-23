using LIB.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace LIB.DTOs.Address
{
    public class UpdateAddressRequest
    {
        [Required(ErrorMessage = "El id es obligatoria")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número es obligatorio")]
        public int Number { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public SideType Side { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [StringLength(5, ErrorMessage = "La longitud del codigo postal no puede ser mayor a 5 caracteres")]
        [MinLength(5, ErrorMessage = "La longitud del codigo postal no puede ser menor a 5 caracteres")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria")]
        [StringLength(150, ErrorMessage = "La longitud de la calle no puede ser mayor a 150 caracteres")]
        public string Street { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria")]
        public IFormFile Image { get; set; }
    }
}
