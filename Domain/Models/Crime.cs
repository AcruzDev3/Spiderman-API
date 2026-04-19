using Domain.Exceptions;

namespace Domain.Models
{
    public class Crime
    {
        private static readonly TimeSpan AllowedFutureSkew = TimeSpan.FromMinutes(5);

        private int _id;
        private Address _address;
        private List<User> _users = new List<User>();
        private List<Criminal> _criminals = new List<Criminal>();
        private CrimeGrade _grade;
        private CrimeType _type;
        private string? _description;
        private DateTime _dateStart;
        private DateTime? _dateEnd;
        private bool _status;

        public int Id { get => this._id; private set => this._id = value;  }
        public Address Address { get => this._address; private set => this._address = value; }
        public List<User> Users { get => this._users; private set => this._users = value; }
        public List<Criminal> Criminals { get => this._criminals; private set => this._criminals = value; }
        public CrimeGrade Grade { get => this._grade; private set => this._grade = value; }
        public CrimeType Type { get => this._type; private set => this._type = value; }
        public string? Description { get => this._description; private set => this._description = value; }
        public DateTime DateStart { get => this._dateStart; private set => this._dateStart = value; }
        public DateTime? DateEnd { get => this._dateEnd; private set => this._dateEnd = value; }
        public bool Status { get => this._status; private set => this._status = value; }

        // Constructor para recoger o modificar un Crimen
        public Crime(int id, Address address, List<User> users, List<Criminal> criminals, CrimeGrade grade, CrimeType type, 
            string? description, bool status, DateTime dateStart, DateTime? dateEnd = null) {
            if (id < 1) throw new DomainValidationException("El id del crimen no es válido");
            this.Id = id;

            if(address == null) throw new DomainValidationException("La dirección del crimen no es válida");
            this.Address = address;

            if(users == null || users.Count == 0) throw new DomainValidationException("El crimen debe tener al menos un héroe asignado");
            this.Users = users;

            if(criminals == null || criminals.Count == 0) throw new DomainValidationException("El crimen debe tener al menos un criminal asignado");
            this.Criminals = criminals;

            if (grade == null) throw new DomainValidationException("El grado del crimen no es válido");
            this.Grade = grade;

            if(type == null) throw new DomainValidationException("El tipo del crimen no es válido");
            this.Type = type;

            if (description != null && description.Length > 300) throw new DomainValidationException("La descripción del crimen no puede tener más de 300 caracteres");
            this.Description = description;

            DateTime nowUtc = DateTime.UtcNow;
            DateTime startUtc = dateStart.ToUniversalTime();
            DateTime? endUtc = dateEnd?.ToUniversalTime();
            
            if(startUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de inicio del crimen no puede ser en el futuro");
            if(endUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de fin del crimen no puede ser en el futuro");

            if (endUtc != null && startUtc > endUtc) throw new DomainValidationException("La fecha de inicio del crimen no puede ser posterior a la fecha de fin");
            if (endUtc != null && endUtc < startUtc) throw new DomainValidationException("La fecha de fin del crimen no puede ser anterior a la fecha de inicio");

            this.DateStart = startUtc;
            this.DateEnd = endUtc;

            if(endUtc != null && status == false) throw new DomainValidationException("El estado del crimen no puede estar sin terminar si existe la fecha fin");
            if(endUtc == null && status == true) throw new DomainValidationException("El estado del crimen no puede estar terminado si no existe la fecha fin");

            this.Status = status;
        }

        // Constructor para crear un crimen
        public Crime(Address address, List<User> users, List<Criminal> criminals, CrimeGrade grade, CrimeType type, 
            string? description, bool status, DateTime dateStart, DateTime? dateEnd = null) {
            if (address == null) throw new DomainValidationException("La dirección del crimen no es válida");
            this.Address = address;

            if (users == null || users.Count == 0) throw new DomainValidationException("El crimen debe tener al menos un héroe asignado");
            this.Users = users;

            if (criminals == null || criminals.Count == 0) throw new DomainValidationException("El crimen debe tener al menos un criminal asignado");
            this.Criminals = criminals;

            if (grade == null) throw new DomainValidationException("El grado del crimen no es válido");
            this.Grade = grade;

            if (type == null) throw new DomainValidationException("El tipo del crimen no es válido");
            this.Type = type;

            if (description != null && description.Length > 300) throw new DomainValidationException("La descripción del crimen no puede tener más de 300 caracteres");
            this.Description = description;

            DateTime nowUtc = DateTime.UtcNow;
            DateTime startUtc = dateStart.ToUniversalTime();
            DateTime? endUtc = dateEnd?.ToUniversalTime();

            if (startUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de inicio del crimen no puede ser en el futuro");
            if (endUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de fin del crimen no puede ser en el futuro");

            if (endUtc != null && startUtc > endUtc) throw new DomainValidationException("La fecha de inicio del crimen no puede ser posterior a la fecha de fin");
            if (endUtc != null && endUtc < startUtc) throw new DomainValidationException("La fecha de fin del crimen no puede ser anterior a la fecha de inicio");

            this.DateStart = startUtc;
            this.DateEnd = endUtc;

            if (endUtc != null && status == false) throw new DomainValidationException("El estado del crimen no puede estar sin terminar si existe la fecha fin");
            if (endUtc == null && status == true) throw new DomainValidationException("El estado del crimen no puede estar terminado si no existe la fecha fin");
            this.Status = status;
        }

        public void Solved() {
            this.Status = true;
            this.DateEnd = DateTime.UtcNow;
        }
    }
}
