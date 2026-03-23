using System.ComponentModel.DataAnnotations;

namespace LIB.DTOs.Criminal {
    public class CreateCriminalRequest {

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El riesgo es obligatorio")]
        public int RiskId { get; set; }

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "La fecha desde es obligatoria")]
        public DateTime Since { get; set; }
    }
}
