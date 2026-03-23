using System.ComponentModel.DataAnnotations;
using LIB.DTOs.Criminal;
using LIB.Models;

namespace LIB.ViewModels
{
    public class CriminalViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public string Risk { get; set; }

        public string? Image { get; set; }

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
