using Domain.Enums;
using Domain.Exceptions;
namespace Domain.Models
{
    public class Address
    {
        private int _id;
        private int _number;
        private SideType _side;
        private string _zipCode;
        private string _street;
        private string _image;

        public int Id { get => this._id; private  set => _id = value; }
        public int Number { get => this._number; private set => this._number = value; }
        public SideType Side { get => this._side; private set => this._side = value; }
        public string ZipCode { get => this._zipCode; private set => this._zipCode = value; }
        public string Street { get => this._street; private set => this._street = value; }
        public string Image { get => this._image; private set => this._image = value; }

        // Constructor para recoger o modificar una dirección
        public Address(int id, int number, SideType side, string zipCode, string street, string image)
        {
            if(id < 0) throw new DomainValidationException("El id de la dirección no es válido");
            this.Id = id;

            if(number < 1) throw new DomainValidationException("El número de la calle de la dirección no es válido");
            this.Number =number;
            this.Side = side;

            if (String.IsNullOrEmpty(zipCode)) throw new DomainValidationException("El código postal de la dirección no es válido");
            if (zipCode.Length != 5) throw new DomainValidationException("El código postal de la dirección debe tener 5 dígitos");
            this.ZipCode = zipCode;

            if (String.IsNullOrEmpty(street)) throw new DomainValidationException("La calle de la dirección no es válida");
            if (street.Length > 150) throw new DomainValidationException("El calle de la dirección no puede ser mayor a 150");
            this.Street = street;

            if (String.IsNullOrEmpty(image)) throw new DomainValidationException("La ruta imagen de la dirección no es válida");
            if (image.Length > 200) throw new DomainValidationException("La ruta de la imagen de la dirección no puede ser mayor a 150");
            this.Image = image;
        }

        // Constructor para modificar una dirección
        public Address(int number, SideType side, string zipCode, string street, string image)
        {
            if(number < 1) throw new DomainValidationException("El número de la calle de la dirección no es válido");
            this.Number = number;
            this.Side = side;

            if (String.IsNullOrEmpty(zipCode)) throw new DomainValidationException("El código postal de la dirección no es válido");
            if(zipCode.Length != 5) throw new DomainValidationException("El código postal de la dirección debe tener 5 dígitos");
            this.ZipCode = zipCode;

            if(String.IsNullOrEmpty(street)) throw new DomainValidationException("La calle de la dirección no es válida");
            if (street.Length > 150) throw new DomainValidationException("El calle de la dirección no puede ser mayor a 150");
            this.Street = street;

            if(String.IsNullOrEmpty(image)) throw new DomainValidationException("La ruta imagen de la dirección no es válida");
            if (image.Length > 200) throw new DomainValidationException("La ruta de la imagen de la dirección no puede ser mayor a 150");
            this.Image = image;
        }
    }
}
