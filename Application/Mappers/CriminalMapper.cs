using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.Criminal;

namespace Application.Mappers
{
    public static class CriminalMapper
    {
        public static CriminalResponse ToResponse(Criminal criminal, CriminalRiskLevelResponse risk) {
            return new CriminalResponse {
                Id = criminal.Id,
                Name = criminal.Name,
                Description = criminal.Description,
                Risk = risk,
                Image = criminal.Image,
                Since = criminal.Since,
            };
        }

        public static Criminal ToModel(CreateCriminalRequest request, CriminalRiskLevel risk,
            string pathImageProfile) {
            return new Criminal(request.Name, request.Description, risk, pathImageProfile, request.Since);
        }

        public static Criminal ToModel(UpdateCriminalRequest request, CriminalRiskLevel risk,
            string pathImageProfile) {
            return new Criminal(request.Name, request.Description, risk, pathImageProfile, request.Since);
        }
    }
}
