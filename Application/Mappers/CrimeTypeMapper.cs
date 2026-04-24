using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.CrimeType;

namespace Application.Mappers
{
    public static class CrimeTypeMapper
    {
        public static CrimeTypeResponse ToResponse(CrimeType crimeType) {
            return new CrimeTypeResponse
            {
                Id = crimeType.Id,
                Name = crimeType.Name,
            };
        }

        public static CrimeType ToModel(CreateCrimeTypeRequest request) {
            return new CrimeType(request.Name);
        }
    }
}
