namespace Infrastructure.Persistence.Entities;

public partial class CrimeGradeEntity
{
    public int CrimeGradeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CrimeEntity> Crimes { get; set; } = new List<CrimeEntity>();
}
