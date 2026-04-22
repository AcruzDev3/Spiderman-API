using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.CriminalRiskLevel;

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

        public static CriminalRiskLevel ToModel(CreateCriminalRiskLevelRequest request) {
            return new CriminalRiskLevel(request.Name);
        }

        public static CriminalRiskLevel ToModel(UpdateCriminalRiskLevelRequest request) {
            return new CriminalRiskLevel(request.Id, request.Name);
        }
    }
}
