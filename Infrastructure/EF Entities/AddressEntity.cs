namespace Infrastructure.EF_Entities;

public partial class AddressEntity
{
    public int AddressId { get; set; }

    public int Number { get; set; }

    public string Side { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<CrimeEntity> Crimes { get; set; } = new List<CrimeEntity>();
}
