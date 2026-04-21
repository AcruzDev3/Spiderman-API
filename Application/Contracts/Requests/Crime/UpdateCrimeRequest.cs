using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Crime
{
    public class UpdateCrimeRequest
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public int GradeId { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Los criminales son obligatorios")]
        public List<int> CriminalIds { get; set; } = null!;

        [Required(ErrorMessage = "Los heroes son obligatorios")]
        public List<int> UserIds { get; set; } = null!;

        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
