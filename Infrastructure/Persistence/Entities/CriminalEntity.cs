namespace Infrastructure.Persistence.Entities;

public partial class CriminalEntity
{
    public int CriminalId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int RiskId { get; set; }

    public string? Image { get; set; }

    public DateTime CriminalSince { get; set; }

    public virtual CriminalRiskLevelEntity Risk { get; set; } = null!;

    public virtual ICollection<CrimeEntity> Crimes { get; set; } = new List<CrimeEntity>();
}
