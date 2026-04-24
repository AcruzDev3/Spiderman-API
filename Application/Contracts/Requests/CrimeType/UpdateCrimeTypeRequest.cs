namespace Application.Contracts.Requests.CrimeType
{
    public class UpdateCrimeTypeRequest
    {
        public int CrimeTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
