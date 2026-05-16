namespace Infrastructure.Persistence.Entities;

public partial class CriminalRiskLevelEntity
{
    public int CriminalRiskLevelId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CriminalEntity> Criminals { get; set; } = new List<CriminalEntity>();
}
