using Domain.Exceptions;

namespace Domain.Models
{
    public class CrimeType
    {
        private int _id;
        private string _name;

        public int Id { get => this._id; private set => this._id = value; }
        
        public string Name { get => this._name; private set => this._name = value; }

        // Constructor para recoger o modificar un tipo de crimen
        public CrimeType(int id, string name) {
            if (id <= 0) throw new DomainValidationException("El id del tipo del crimen no es válido");
            this.Id = id;

            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del tipo del crimen no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del tipo de crimen no puede ser mayor a 50 caracteres");
            this.Name = name;
        }

        // Constructor para crear un tipo de crimen
        public CrimeType(string name) {
            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del tipo del crimen no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del tipo de crimen no puede ser mayor a 50 caracteres");
            this.Name = name;
        }

        public void ChangeName(string name) {
            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del tipo del crimen no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del tipo de crimen no puede ser mayor a 50 caracteres");
            this.Name = name;
        }
    }
}
