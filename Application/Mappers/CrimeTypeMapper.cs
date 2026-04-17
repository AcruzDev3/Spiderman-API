using Domain.Models;
using Application.Contracts.Responses;

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
    }
}
