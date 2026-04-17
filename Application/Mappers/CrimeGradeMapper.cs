using Domain.Models;
using Application.Contracts.Responses;

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
    }
}
