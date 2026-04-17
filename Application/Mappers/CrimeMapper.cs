using Domain.Models;
using Application.Contracts.Responses;

namespace Application.Mappers
{
    public static class CrimeMapper
    {
        public static CrimeResponse ToResponse(Crime crime, AddressResponse address, 
            List<CriminalResponse> criminals, List<UserResponse> heroes, CrimeGradeResponse grade, 
            CrimeTypeResponse type) {
            return new CrimeResponse
            {
                Id = crime.Id,
                Address = address,
                Criminals = criminals,
                Heroes = heroes,
                Grade = grade,
                Type = type,
                Description = crime.Description,
                DateStart = crime.DateStart,
                DateEnd = crime.DateEnd
            };
        }
    }
}
