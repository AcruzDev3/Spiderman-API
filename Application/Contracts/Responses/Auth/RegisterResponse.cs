namespace Application.Contracts.Responses.Auth
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }
}
