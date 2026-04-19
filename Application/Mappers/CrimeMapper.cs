using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.Crime;

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

        public static Crime ToModel(CreateCrimeRequest request, Address address, 
            List<Criminal> criminals, List<User> heroes, CrimeGrade grade, CrimeType type) {
            return new Crime(address, heroes, criminals, grade, type, request.Description, false, DateTime.UtcNow);
        }

        public static Crime ToModel(UpdateCrimeRequest request, Address address,
            List<Criminal> criminals, List<User> heroes, CrimeGrade grade, CrimeType type) {
            return new Crime(address, heroes, criminals, grade, type, request.Description, request.Status, request.DateStart, request.DateEnd);
        }
    }
}
