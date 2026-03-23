using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LIB.DTOs.Criminal
{
    public class UpdateCriminalRequest
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El riesgo es obligatorio")]
        public string Risk { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "La fecha desde es obligatoria")]
        public DateTime Since { get; set; }
    }
}
