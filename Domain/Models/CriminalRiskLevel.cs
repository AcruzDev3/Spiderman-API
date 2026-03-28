using Domain.Exceptions;

namespace Domain.Models
{
    public class CriminalRiskLevel
    {
        private int _id;
        private string _name;

        public int Id { get => this._id; set => this._id = value; }
        public string Name { get => this._name; set => this._name = value; }

        // Constructor para recibir o modificar un nivel de peligrosidad
        public CriminalRiskLevel(int id, string name) {
            if (id <= 0) throw new DomainValidationException("El id del nivel de peligrosidad del criminal no es válido");
            this.Id = id;

            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del nivel de peligrosidad del criminal no es válido");
            if (name.Length > 20) throw new DomainValidationException("El nombre del nivel de peligrosidad del crimen no puede ser mayor a 20 caracteres");
            this._name = name;
        }

        // Constructor para crear un nivel de peligrosidad
        public CriminalRiskLevel(string name) {
            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del nivel de peligrosidad del criminal no es válido");
            if (name.Length > 20) throw new DomainValidationException("El nombre del nivel de peligrosidad del crimen no puede ser mayor a 20 caracteres");
            this._name = name;
        }
    }
}
