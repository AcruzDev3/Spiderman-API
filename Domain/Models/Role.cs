using Domain.Exceptions;

namespace Domain.Models
{
    public class Role
    {
        private int _id;
        private string _name;

        public int Id { get => this._id; private set => this._id = value; }
        public string Name { get => this._name; private set => this._name = value; }

        // Constructor para recoger o modificar un rol de usuario
        public Role(int id, string name) {
            if(id <= 0) throw new DomainValidationException("El id del rol no es válido");
            this._id = id;

            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del rol no es válido");
            if (name.Length > 100) throw new DomainValidationException("El nombre del rol no puede ser mayor a 100 caracteres");
            this._name = name;
        }

        // Constructor para crear un rol de usuario
        public Role(string name) {
            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del rol no es válido");
            if (name.Length > 100) throw new DomainValidationException("El nombre del rol no puede ser mayor a 100 caracteres");
            this._name = name;
        }
    }
}
