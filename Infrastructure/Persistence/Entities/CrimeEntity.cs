namespace Infrastructure.Persistence.Entities;

public partial class CrimeEntity
{
    public int CrimeId { get; set; }

    public int AddressId { get; set; }

    public int GradeId { get; set; }

    public int TypeId { get; set; }

    public string? Description { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public bool Status { get; set; }

    public virtual AddressEntity Address { get; set; } = null!;

    public virtual CrimeGradeEntity Grade { get; set; } = null!;

    public virtual CrimeTypeEntity Type { get; set; } = null!;

    public virtual ICollection<CriminalEntity> Criminals { get; set; } = new List<CriminalEntity>();

    public virtual ICollection<UserEntity> Heroes { get; set; } = new List<UserEntity>();
}

