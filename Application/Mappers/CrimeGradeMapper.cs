using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.CrimeGrade;

namespace Application.Mappers
{
    public static class CrimeGradeMapper
    {
        public static CrimeGradeResponse ToResponse(CrimeGrade crimeGrade) {
            return new CrimeGradeResponse
            {
                Id = crimeGrade.Id,
                Name = crimeGrade.Name,
            };
        }

        public static CrimeGrade ToModel(CreateCrimeGradeRequest request) {
            return new CrimeGrade(request.Name);
        }
    }
}
