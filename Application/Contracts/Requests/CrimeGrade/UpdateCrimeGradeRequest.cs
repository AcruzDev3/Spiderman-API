namespace Application.Contracts.Requests.CrimeGrade
{
    public class UpdateCrimeGradeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
