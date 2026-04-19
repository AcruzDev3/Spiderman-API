using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CriminalRepository : ICriminalRepository
    {
        private readonly SpidermanContext _context;

        public CriminalRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<Criminal?> GetById(int id) {
            CriminalEntity? entity =  await this._context.Criminals.AsNoTracking()
                                        .FirstOrDefaultAsync(c => c.CriminalId == id);
            if (entity == null) return null;
            else return await this.GetCriminalModel(entity);
        }

        public async Task<List<Criminal>?> GetByIds(List<int> ids) {
            List<CriminalEntity>? entities =  await this._context.Criminals
                .Where(c => ids.Contains(c.CriminalId))
                .ToListAsync();
            List<Criminal> criminals = new List<Criminal>();

            foreach(CriminalEntity entity in entities) criminals.Add(await this.GetCriminalModel(entity));

            return criminals;
        }

        public async Task<List<Criminal>?> GetAll() {
            List<CriminalEntity> entites = await this._context.Criminals.AsNoTracking()
                                            .ToListAsync();
            List<Criminal> criminals = new List<Criminal>();
            foreach (CriminalEntity entite in entites) criminals.Add(await this.GetCriminalModel(entite));

            return criminals;
        }

        public async Task<List<Crime>?> GetCrimes(Criminal model) {
             List<CrimeEntity>? crimesEntities = await this._context.Crimes
                .Where(c => c.Criminals.Any(cr => cr.CriminalId == model.Id))
                .ToListAsync();

            List<Crime> crimes = new List<Crime>();
            foreach(CrimeEntity crimeEntity in crimesEntities)
                crimes.Add(await this.GetCrimeModel(crimeEntity));

            return crimes;
        }

        public async Task<bool> Exists(string name) {
            try {
                CriminalEntity? entity = await this._context.Criminals
                            .FirstOrDefaultAsync(m => m.Name
                            .Equals(name, StringComparison.OrdinalIgnoreCase));

                if (entity == null) return false;
                else return true;
            } catch (Exception) {
                throw;
            }
        }

        private async Task<Crime> GetCrimeModel(CrimeEntity entity) {
            CrimeGrade grade = CrimeGradeMapper.ToDomain(entity.Grade);
            CrimeType type = CrimeTypeMapper.ToDomain(entity.Type);
            Address address = AddressMapper.ToDomain(entity.Address);

            List<User> users = new List<User>();
            foreach (UserEntity userEntity in entity.Heroes) {
                Role role = RoleMapper.ToDomain(userEntity.Role);
                users.Add(UserMapper.ToDomain(userEntity, role));
            }

            List<Criminal> criminals = new List<Criminal>();
            foreach (CriminalEntity criminalEntity in entity.Criminals) {
                CriminalRiskLevel risk = CriminalRiskLevelMapper.ToDomain(criminalEntity.Risk);
                criminals.Add(CriminalMapper.ToDomain(criminalEntity, risk));
            }

            return CrimeMapper.ToDomain(entity, users, criminals, address, grade, type);
        }

        public async Task<CriminalRiskLevel?> GetCriminalRiskLevelAsync(int idRisk) {
            try {
                CriminalRiskLevelEntity? entity = await this._context.CriminalRiskLevels
                    .FirstOrDefaultAsync(r => r.CriminalRiskLevelId == idRisk);
                if (entity != null) return CriminalRiskLevelMapper.ToDomain(entity);
                else return null;
            } catch (Exception) {
                throw;
            }
        }

        public async Task Add(Criminal criminal) {
            await this._context.Criminals.AddAsync(CriminalMapper.ToEntity(criminal));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el criminal en base de datos");
        }

        public async Task Update(Criminal criminal) {
            this._context.Criminals.Update(CriminalMapper.ToEntity(criminal));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el criminal en base de datos");
        }

        public async Task Delete(Criminal criminal) {
            this._context.Criminals.Remove(CriminalMapper.ToEntity(criminal));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el criminal en base de datos");
        }

        private async Task<Criminal> GetCriminalModel(CriminalEntity entity) {
            CriminalRiskLevel risk = CriminalRiskLevelMapper.ToDomain(entity.Risk);
            return CriminalMapper.ToDomain(entity, risk);
        }
    }
}
