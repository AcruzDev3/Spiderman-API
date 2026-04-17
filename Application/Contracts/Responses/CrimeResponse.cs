namespace Application.Contracts.Responses
{
    public class CrimeResponse
    {
        public int Id { get; set; }
        public AddressResponse Address { get; set; }
        public List<UserResponse> Heroes { get; set; }
        public List<CriminalResponse> Criminals { get; set; }
        public CrimeGradeResponse Grade { get; set; }
        public CrimeTypeResponse Type { get; set; }
        public string? Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool Status { get; set; }
    }
}
