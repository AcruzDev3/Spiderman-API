namespace Application.Contracts.Responses
{
    public class CrimeResponse
    {
        public int Id { get; set; }
        public AddressResponse Address { get; set; } = null!;
        public List<UserResponse> Heroes { get; set; } = null!;
        public List<CriminalResponse> Criminals { get; set; } = null!;
        public CrimeGradeResponse Grade { get; set; } = null!;
        public CrimeTypeResponse Type { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool Status { get; set; }
    }
}
