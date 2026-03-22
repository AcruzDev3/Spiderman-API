namespace LIB.DTOs.User
{
    public class CreateUserRequest
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
