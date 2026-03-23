using LIB.Models;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;
using LIB.DTOs.Crime;

namespace LIB.ViewModels
{
    public class CrimeViewModel
    {
        public int Id { get; set; }
        public AddressViewModel Address { get; set; }

        public string Grade { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public bool Status { get; set; }

        List<CriminalViewModel> Criminals = new List<CriminalViewModel>();

        List<UserViewModel> Users = new List<UserViewModel>();

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
                this.Criminals = model.Criminals.Select(c => new CriminalViewModel(c)).ToList();
                this.Users = model.Heroes.Select(h => new UserViewModel(h)).ToList();
            } catch(Exception) {
                throw;
            }
        }

        public CrimeViewModel(CreateCrimeRequest dto) {
            this.Description = dto.Description;
        }
    }
}