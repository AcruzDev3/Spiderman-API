using System.ComponentModel.DataAnnotations;

namespace LIB.DTOs.Crime
{
    public class UpdateCrimeRequest
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public int GradeName { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public int TypeName { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public int? AddressId { get; set; }

        [Required(ErrorMessage = "Los criminales son obligatorios")]
        public List<int>? CriminalsIds { get; set; }

        [Required(ErrorMessage = "Los heroes son obligatorios")]
        public List<int>? UsersIds { get; set; }
    }
}
