using Domain.Models;
using Infrastructure.EF_Entities;

namespace Infrastructure.Mappers
{
    public static class CriminalMapper
    {
        public static Criminal ToDomain(CriminalEntity entity, CriminalRiskLevel risk) {
            return new Criminal(entity.CriminalId, entity.Name, entity.Description, risk, entity.Image, entity.CriminalSince);
        }

        public static CriminalEntity ToEntity(Criminal criminal) {
            return new CriminalEntity {
                CriminalId = criminal.Id,
                Name = criminal.Name,
                Description = criminal.Description,
                RiskId = criminal.Risk.Id,
                Image = criminal.Image,
                CriminalSince = criminal.Since
            };
        }
    }
}
