using Domain.Models;
using Application.Contracts.Responses;

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
    }
}
