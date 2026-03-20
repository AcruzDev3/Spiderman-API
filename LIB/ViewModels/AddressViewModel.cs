using LIB.Models;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;
using LIB.DTOs;

namespace LIB.ViewModels
{
    public class AddressViewModel
    {
        [Required(ErrorMessage = "El id de la direccion es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El id de la vista de la dirección no es válido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de la dirección es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El numero de la dirrecion no es válido")]
        public int Number { get; set; }

        [Required(ErrorMessage = "La parte es obligatorio")]
        [EnumDataType(typeof(SideType), ErrorMessage = "El formato de la parte no es válida")]
        public SideType Side { get; set; }

        [Required(ErrorMessage = "El codigo postal es obligatorio")]
        [StringLength(5, ErrorMessage = "La longitud del codigo postal no puede ser mayor a 5 caracteres")]
        [MinLength(5, ErrorMessage = "La longitud del codigo postal no puede ser menor a 5 caracteres")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria")]
        [StringLength(150, ErrorMessage = "La longitud de la calle no puede ser mayor a 150 caracteres")]
        public string Street { get; set; }
        [StringLength(150, ErrorMessage = "La longitud de la ruta de la imagen no puede ser mayor a 150 caracteres")]
        public string? Image { get; set; }

        /// <summary>
        /// Constructor que mapea un modelo a un ViewModel
        /// </summary>
        /// <param name="model">Address</param>
        public AddressViewModel(Address model) {
            try {
                if (model == null) throw new Exception("La dirección no puede ser nulo");
                if (!Enum.TryParse<SideType>(model.Side, out SideType validSide))
                    throw new Exception("El side no es válido");

                this.Id = model.AddressId;
                this.ZipCode = model.ZipCode;
                this.Number = model.Number;
                this.Street = model.Street;
                this.Side = validSide;
            } catch (Exception) {
                throw;
            }
        }
        /// <summary>
        /// Constructor que mapea un DTO a un ViewModel
        /// </summary>
        /// <param name="dto">CreateAddressRequest</param>
        public AddressViewModel(CreateAddressRequest dto) {
            try {
                if (dto == null) throw new Exception("La dirección no puede ser nulo");
                
                if (!Enum.TryParse<SideType>(dto.Side, out SideType validSide))
                    throw new Exception("El side no es válido");
                this.Number = dto.Number;
                this.ZipCode = dto.ZipCode;
                this.Street = dto.Street;
                this.Side = validSide;
                this.Image = dto.Image;
            } catch (Exception) {
                throw;
            }
        }
    }
}
