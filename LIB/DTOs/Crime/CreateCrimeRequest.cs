namespace LIB.DTOs.Crime
{
    public class CreateCrimeRequest
    {
        public string? GradeName { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public int AddressId { get; set; }
            public List<int>? CriminalIds { get; set; }
        public List<int>? UserIds { get; set; }
    }
}
