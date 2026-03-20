using LIB.Interfaces;
using LIB.Models;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;
using LIB.DTOs;

namespace LIB.ViewModels
{
    public class CrimeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La dirección del crimen es obligatoria")]
        public AddressViewModel Address { get; set; }

        [Required(ErrorMessage = "El grado del crimen es obligatorio")]
        public string Grade { get; set; }

        [Required(ErrorMessage = "El tipo del crimen es obligatorio")]
        public string Type { get; set; }

        [StringLength(300, ErrorMessage = "La longitud de la descripcion no puede ser mayor a 300 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La fecha de inicio del crimen es obligatoria")]
        [DataType(DataType.DateTime, ErrorMessage = "El formato de la fecha de inicio no es válido")]
        public DateTime Start { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "El formato de la fecha de inicio no es válido")]
        public DateTime? End { get; set; }

        [Required(ErrorMessage = "El estado del crimen es obligatorio")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "No puede haber un crimen sin criminales")]
        List<CriminalViewModel> Criminals = new List<CriminalViewModel>();
        public CrimeViewModel(Crime model) {
            try {
                if (model == null) throw new Exception("El crimen no puede ser nulo");

                this.Id = model.CrimeId;
                this.Address = new AddressViewModel(model.Address);
                this.Grade = model.Grade.Name;
                this.Type = model.Type.Name;
                this.Description = model.Description;
                this.Start = model.DateStart;
                this.Status = model.Status;
                this.End = model?.DateEnd;
            } catch(Exception) {
                throw;
            }
        }

        public CrimeViewModel(CreateCrimeRequest dto, AddressViewModel address, CrimeGrade grade, CrimeType type, List<CriminalViewModel> criminals) {
            
            if(address == null) throw new Exception("La dirección no es válida");
            if(grade == null) throw new Exception("El grado del crimen no es válido");
            if(type == null) throw new Exception("El tipo del crimen no es válido");
            if(criminals == null || criminals.Count == 0) throw new Exception("El crimen debe tener al menos un criminal asociado");

            this.Address = address;
            this.Grade = grade.Name;
            this.Type = type.Name;
            this.Description = dto.Description;
            this.Criminals = criminals;
        }
    }
}