namespace Infrastructure.EF_Entities;

public partial class UserEntity
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Image { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;

    public virtual ICollection<CrimeEntity> Crimes { get; set; } = new List<CrimeEntity>();
}
