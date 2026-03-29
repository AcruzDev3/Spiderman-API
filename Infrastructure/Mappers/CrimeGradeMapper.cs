using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class CrimeGradeMapper
    {
        public static CrimeGrade ToDomain(CrimeGradeEntity entity) {
            return new CrimeGrade(entity.CrimeGradeId, entity.Name);
        }

        public static CrimeGradeEntity ToEntity(CrimeGrade grade) {
            return new CrimeGradeEntity {
                CrimeGradeId = grade.Id,
                Name = grade.Name
            };
        }
    }
}
