using System.ComponentModel.DataAnnotations;
using API.DTOs;
using LIB.Models;

namespace LIB.ViewModels
{
    public class CriminalViewModel
    {
        [Required(ErrorMessage = "El id del criminal es obligatorio")] 
        [Range(0, int.MaxValue, ErrorMessage = "El id del criminal no es válido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "La longitud del nombre no puede ser mayor a 50 caracteres")]
        public string Name { get; set; }
        
        [StringLength(300, ErrorMessage = "La longitud de la descripcion no puede ser mayor a 300 caracteres")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "El riesgo es obligatorio")]
        public string Risk { get; set; }

        [StringLength(255, ErrorMessage = "La longitud de la ruta de la imagen no puede ser mayor a 255 caracteres")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.DateTime, ErrorMessage = "El formato de la fecha no es válido")]
        public DateTime Since { get; set; }

        /// <summary>
        /// Constructor que mapea un modelo Criminal a un CriminalViewModel
        /// </summary>
        /// <param name="model">Criminal, modelo de la base de datos</param>
        public CriminalViewModel(Criminal model) {
            try {
                if (model == null) throw new Exception("El modelo no puede ser nulo");
                
                if(model.Risk == null) throw new Exception("El riesgo del criminal no puede ser nulo");
                if(model.CriminalSince == DateTime.MinValue) throw new Exception("La fecha de inicio del criminal no puede ser nula");

                this.Id = model.CriminalId;
                this.Name = model.Name;
                this.Description = model?.Description;
                this.Risk = model.Risk.Name;
                this.Image = model?.Image;
                this.Since = model.CriminalSince;
            } catch(Exception) {
                throw;
            }
        }
        /// <summary>
        /// Constructor que mapea un CreateCriminalRequest a un CriminalViewModel
        /// Recibe un DTO de creación de criminal
        /// </summary>
        /// <param name="dto">CreateCriminalRequest</param>
        public CriminalViewModel(CreateCriminalRequest dto, CriminalRiskLevel risk) {
            this.Name = dto.Name; 
            this.Description = dto.Description;
            this.Risk = risk.Name;
            this.Image = dto.Image;
            this.Since = dto.Since;
        }
    }
}
