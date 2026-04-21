using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Crime
{
    public class CreateCrimeRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public int GradeId { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public int TypeId { get; set; }

        [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public int AddressId { get; set; } 

        [Required(ErrorMessage = "Los criminales son obligatorios")]
        public List<int> CriminalIds { get; set; } = null!;

        [Required(ErrorMessage = "Los heroes son obligatorios")]
        public List<int> UserIds { get; set; } = null!;
    } 
}
