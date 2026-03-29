namespace Infrastructure.EF_Entities;

public partial class CrimeTypeEntity
{
    public int CrimeTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CrimeEntity> Crimes { get; set; } = new List<CrimeEntity>();
}