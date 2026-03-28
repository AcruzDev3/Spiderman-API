using Domain.Exceptions;

namespace Domain.Models
{
    public class Criminal
    {
        private int _id;
        private string _name;
        private string? _description;
        private CriminalRiskLevel _risk;
        private string _image;
        private DateTime _since;

        public int Id { get => this._id; private set => this._id = value; }
        public string Name { get => this._name; private set => this._name = value; }
        public string? Description { get => this._description; private set => this._description = value; }
        public CriminalRiskLevel Risk { get => this._risk; private set => this._risk = value; }
        public string Image { get => this._image; private set => this._image = value; }
        public DateTime Since { get => this._since; private set => this._since = value; }

        public Criminal(int id, string name, string? description, CriminalRiskLevel risk, string image, 
            DateTime since) {
            if (id < 0) throw new DomainValidationException("El id del criminal no es válido");
            this.Id = id;

            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del criminal no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del criminal no puede ser mayor a 50 caracteres");
            this.Name = name;

            if (description != null && description.Length > 300) throw new DomainValidationException("La descripción del criminal no puede tener más de 300 caracteres");
            this.Description = description;

            if (risk == null) throw new DomainValidationException("El nivel de riesgo del criminal no es válido");
            this.Risk = risk;

            if (String.IsNullOrEmpty(image)) throw new DomainValidationException("La ruta imagen del criminal no es válida");
            if (image.Length > 200) throw new DomainValidationException("La ruta de la imagen del criminal no puede ser mayor a 255 caracteres");
            this.Image = image;

            DateTime nowUtc = DateTime.UtcNow;
            DateTime sinceUtc = since.ToUniversalTime();

            if (sinceUtc > nowUtc) throw new DomainValidationException("La fecha desde la que el criminal es conocido no puede ser en el futuro");
            this.Since = sinceUtc;
        }

        public Criminal(string name, string? description, CriminalRiskLevel risk, string image,
            DateTime since) {
            if (String.IsNullOrEmpty(name)) throw new DomainValidationException("El nombre del criminal no es válido");
            if (name.Length > 50) throw new DomainValidationException("El nombre del criminal no puede ser mayor a 50 caracteres");
            this.Name = name;

            if (description != null && description.Length > 300) throw new DomainValidationException("La descripción del criminal no puede tener más de 300 caracteres");
            this.Description = description;

            if (risk == null) throw new DomainValidationException("El nivel de riesgo del criminal no es válido");
            this.Risk = risk;

            if (String.IsNullOrEmpty(image)) throw new DomainValidationException("La ruta imagen del criminal no es válida");
            if (image.Length > 200) throw new DomainValidationException("La ruta de la imagen del criminal no puede ser mayor a 255 caracteres");
            this.Image = image;

            DateTime nowUtc = DateTime.UtcNow;
            DateTime sinceUtc = since.ToUniversalTime();

            if (sinceUtc > nowUtc) throw new DomainValidationException("La fecha desde la que el criminal es conocido no puede ser en el futuro");
            this.Since = sinceUtc;
        }
    }
}
