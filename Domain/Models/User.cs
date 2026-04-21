using Domain.Exceptions;

namespace Domain.Models
{
    public class User
    {
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private Role _role;
        private string _image;

        public int Id { get => this._id; private set => this._id = value;  }
        public string Name { get => this._name; private set => this._name = value; }
        public string Email { get => this._email; private set => this._email = value; }
        public string Password { get => this._password; private set => this._password = value; }
        public Role Role { get => this._role; private set => this._role = value; }
        public string Image { get => this._image; private set => this._image = value; }
        
        public User (int id, string name, string email, string password, Role role, string image) {
            if (id < 0) throw new DomainValidationException("El id del usuario no es válido");
            this.Id = id;

            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del usuario no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del usuario no puede ser mayor a 50 caracteres");
            this.Name = name;

            if (String.IsNullOrEmpty(email)) throw new DomainValidationException("El email del usuario no es válido");
            if (email.Length > 255) throw new DomainValidationException("El email del usuario no puede ser mayor a 255 caracteres");
            this.Email = email;

            if(String.IsNullOrEmpty(password)) throw new DomainValidationException("La contraseña del usuario no es válida");
            if(password.Length > 255) throw new DomainValidationException("La contraseña del usuario no puede ser mayor a 255 caracteres");
            this.Password = password;

            if (role == null) throw new DomainValidationException("El rol del usuario no es válido");
            this.Role = role;

            if (String.IsNullOrEmpty(image)) throw new DomainValidationException("La imagen del usuario no es válida");
            if (image.Length > 200) throw new DomainValidationException("La imagen no puede ser mayor a 300 caracteres");
            this.Image = image;
        }

        public User(string name, string email, string password, Role role, string image) {
            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del usuario no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del usuario no puede ser mayor a 50 caracteres");
            this.Name = name;

            if (String.IsNullOrEmpty(email)) throw new DomainValidationException("El email del usuario no es válido");
            if (email.Length > 255) throw new DomainValidationException("El email del usuario no puede ser mayor a 255 caracteres");
            this.Email = email;

            if (String.IsNullOrEmpty(password)) throw new DomainValidationException("La contraseña del usuario no es válida");
            if (password.Length > 255) throw new DomainValidationException("La contraseña del usuario no puede ser mayor a 255 caracteres");
            this.Password = password;

            if (role == null) throw new DomainValidationException("El rol del usuario no es válido");
            this.Role = role;

            if (String.IsNullOrEmpty(image)) throw new DomainValidationException("La imagen del usuario no es válida");
            if (image.Length > 200) throw new DomainValidationException("La imagen no puede ser mayor a 300 caracteres");
            this.Image = image;
        }

        public void ChangePassword(string password) {
            this.Password = password;
        }

        public void ChangeEmail(string email) {
            this.Email = email;
        }
    }
}