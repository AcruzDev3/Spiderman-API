using Domain.Models;
using Application.Contracts.Responses;

namespace Application.Mappers
{
    public static class CriminalRiskLevelMapper
    {
        public static CriminalRiskLevelResponse ToResponse(CriminalRiskLevel criminalRiskLevel) {
            return new CriminalRiskLevelResponse
            {
                Id = criminalRiskLevel.Id,
                Name = criminalRiskLevel.Name,
            };
        }
    }
}
