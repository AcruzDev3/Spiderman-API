using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class CriminalRiskLevelMapper
    {
        public static CriminalRiskLevel ToDomain(CriminalRiskLevelEntity entity) {
            return new CriminalRiskLevel(entity.CriminalRiskLevelId, entity.Name);
        }

        public static CriminalRiskLevelEntity ToEntity(CriminalRiskLevel risk) {
            return new CriminalRiskLevelEntity {
                CriminalRiskLevelId = risk.Id,
                Name = risk.Name
            };
        }
    }
}
