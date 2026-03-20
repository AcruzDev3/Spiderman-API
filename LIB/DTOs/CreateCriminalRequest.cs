namespace API.DTOs {
    public class CreateCriminalRequest {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Risk { get; set; }
        public string? Image { get; set; }
        public DateTime Since { get; set; }
    }
}
