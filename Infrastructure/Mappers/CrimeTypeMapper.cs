using Domain.Models;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Mappers
{
    public static class CrimeTypeMapper
    {
        public static CrimeType ToDomain(CrimeTypeEntity entity) {
            return new CrimeType(entity.CrimeTypeId, entity.Name);
        }

        public static CrimeTypeEntity ToEntity(CrimeType type) {
            return new CrimeTypeEntity {
                CrimeTypeId = type.Id,
                Name = type.Name
            };
        }
    }
}
