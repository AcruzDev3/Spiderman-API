namespace Application.Contracts.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public RoleResponse Role { get; set; } = null!;
        public string Image { get; set; } = null!;

    }
}
