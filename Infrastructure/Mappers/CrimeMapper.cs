using Application.Services;
using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class CrimeMapper
    {
        public static Crime ToDomain(CrimeEntity entity, List<User> users, List<Criminal> criminals, Address address, CrimeGrade grade, CrimeType type) {
            return new Crime(entity.CrimeId, address, users, criminals, grade, type, entity.Description, entity.Status, entity.DateStart, entity.DateEnd);
        }

        public static CrimeEntity ToEntity(Crime crime, List<UserEntity> usersEntities, List<CriminalEntity> criminalsEntities) {
            return new CrimeEntity {
                CrimeId = crime.Id,
                AddressId = crime.Address.Id,
                Heroes = usersEntities,
                Criminals = criminalsEntities,
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
