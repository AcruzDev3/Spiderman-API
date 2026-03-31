namespace Application.Contracts.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleResponse Role { get; set; }
        public string Image { get; set; }
        
    }
}
