namespace Application.Contracts.Responses
{
    public class CriminalResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public CriminalRiskLevelResponse Risk { get; set; }
        public string Image { get; set; }
        public DateTime Since { get; set; }
    }
}
