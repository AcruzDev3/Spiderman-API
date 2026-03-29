using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class CrimeMapper
    {
        public static Crime ToDomain(CrimeEntity entity, Address address, CrimeGrade grade, CrimeType type) {
            return new Crime(entity.CrimeId, address, grade, type, entity.Description, entity.Status, entity.DateStart, entity.DateEnd);
        }

        public static CrimeEntity ToEntity(Crime crime) {
            return new CrimeEntity {
                CrimeId = crime.Id,
                AddressId = crime.Address.Id,
                GradeId = crime.Grade.Id,
                TypeId = crime.Type.Id,
                Description = crime.Description,
                Status = crime.Status,
                DateStart = crime.DateStart,
                DateEnd = crime.DateEnd
            };
        }

    }
}
