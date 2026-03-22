namespace LIB.DTOs.Crime
{
    public class UpdateCrimeRequest
    {
        public int Id { get; set; }
        public string? GradeName { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public int AddressId { get; set; }
        public List<int>? CriminalsIds { get; set; }
    }
}
