using Domain.Exceptions;

namespace Domain.Models
{
    public class Crime
    {
        private static readonly TimeSpan AllowedFutureSkew = TimeSpan.FromMinutes(5);

        private int _id;
        private Address _address;
        private CrimeGrade _grade;
        private CrimeType _type;
        private string? _description;
        private DateTime _dateStart;
        private DateTime? _dateEnd;
        public bool _status;

        public int Id { get; private set;  }
        public Address Address { get; private set; }
        public CrimeGrade Grade { get; private set; }
        public CrimeType Type { get; private set; }
        public string? Description { get; private set; }
        public DateTime DateStart { get; private set; }
        public DateTime? DateEnd { get; private set; }
        public bool Status { get; private set; }

        // Constructor para recoger o modificar un Crimen
        public Crime(int id, Address address, CrimeGrade grade, CrimeType type, 
            string? description, bool status, DateTime dateStart, DateTime? dateEnd = null) {
            if (id < 1) throw new DomainValidationException("El id del crimen no es válido");
            this._id = id;

            if(address == null) throw new DomainValidationException("La dirección del crimen no es válida");
            this._address = address;

            if (grade == null) throw new DomainValidationException("El grado del crimen no es válido");
            this._grade = grade;

            if(type == null) throw new DomainValidationException("El tipo del crimen no es válido");
            this._type = type;

            if (description != null && description.Length > 300) throw new DomainValidationException("La descripción del crimen no puede tener más de 300 caracteres");
            this._description = description;

            DateTime nowUtc = DateTime.UtcNow;
            DateTime startUtc = dateStart.ToUniversalTime();
            DateTime? endUtc = dateEnd?.ToUniversalTime();
            
            if(startUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de inicio del crimen no puede ser en el futuro");
            if(endUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de fin del crimen no puede ser en el futuro");

            if (endUtc != null && startUtc > endUtc) throw new DomainValidationException("La fecha de inicio del crimen no puede ser posterior a la fecha de fin");
            if (endUtc != null && endUtc < startUtc) throw new DomainValidationException("La fecha de fin del crimen no puede ser anterior a la fecha de inicio");

            this._dateStart = startUtc;
            this._dateEnd = endUtc;

            if(endUtc != null && status == false) throw new DomainValidationException("El estado del crimen no puede estar sin terminar si existe la fecha fin");
            if(endUtc == null && status == true) throw new DomainValidationException("El estado del crimen no puede estar terminado si no existe la fecha fin");

            this._status = status;
        }

        // Constructor para crear un crimen
        public Crime(Address address, CrimeGrade grade, CrimeType type,
            string? description, bool status, DateTime dateStart, DateTime? dateEnd = null) {
            if (address == null) throw new DomainValidationException("La dirección del crimen no es válida");
            this._address = address;
            
            if (grade == null) throw new DomainValidationException("El grado del crimen no es válido");
            this._grade = grade;

            if (type == null) throw new DomainValidationException("El tipo del crimen no es válido");
            this._type = type;

            if (description != null && description.Length > 300) throw new DomainValidationException("La descripción del crimen no puede tener más de 300 caracteres");
            this._description = description;

            DateTime nowUtc = DateTime.UtcNow;
            DateTime startUtc = dateStart.ToUniversalTime();
            DateTime? endUtc = dateEnd?.ToUniversalTime();

            if (startUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de inicio del crimen no puede ser en el futuro");
            if (endUtc > nowUtc.Add(AllowedFutureSkew)) throw new DomainValidationException("La fecha de fin del crimen no puede ser en el futuro");

            if (endUtc != null && startUtc > endUtc) throw new DomainValidationException("La fecha de inicio del crimen no puede ser posterior a la fecha de fin");
            if (endUtc != null && endUtc < startUtc) throw new DomainValidationException("La fecha de fin del crimen no puede ser anterior a la fecha de inicio");

            this._dateStart = startUtc;
            this._dateEnd = endUtc;

            if (endUtc != null && status == false) throw new DomainValidationException("El estado del crimen no puede estar sin terminar si existe la fecha fin");
            if (endUtc == null && status == true) throw new DomainValidationException("El estado del crimen no puede estar terminado si no existe la fecha fin");
            this._status = status;
        }
    }
}
