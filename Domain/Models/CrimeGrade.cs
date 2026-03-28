using Domain.Exceptions;

namespace Domain.Models
{
    public class CrimeGrade
    {
        private int _id;
        private string _name;
        
        public int Id { get => this._id; private set => _id = value; }
        public string Name { get => this._name; private set => _name = value; }

        // Constructor para recoger o modificar un grado
        public CrimeGrade(int id, string name)
        {
            if(id < 0) throw new DomainValidationException("El id del grado del crimen no es válido");
            this.Id = id;

            if (String.IsNullOrWhiteSpace(name)) throw new DomainValidationException("El nombre del grado del crimen no es válido");
            if (name.Length > 50) throw new DomainValidationException("EL nombre del grado del crimen no puede ser mayor a 150 carácteres");
            this.Name = name; 
        }

        // Constructor para modificar un grado 
        public CrimeGrade(string name) {
            if(String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del grado del crimen no es válido");
            if (name.Length > 50) throw new DomainValidationException("EL nombre del grado del crimen no puede ser mayor a 150 carácteres");
            this.Name = name;
        }
    }
}
