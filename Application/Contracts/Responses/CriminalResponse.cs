namespace Application.Contracts.Responses
{
    public class CriminalResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public CriminalRiskLevelResponse Risk { get; set; } = null!;
        public string Image { get; set; } = null!;
        public DateTime Since { get; set; }
    }
}
