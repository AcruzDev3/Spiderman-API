using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CrimeRepository : ICrimeRepository
    {
        private readonly SpidermanContext _context;

        public CrimeRepository(SpidermanContext context)
        {
            this._context = context;
        }

        public async Task<Crime?> GetById(int id) {
            CrimeEntity? entity = await this._context.Crimes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CrimeId == id);

            if(entity == null) return null;
            else return await this.GetCrimeModel(entity);
        }

        public async Task<List<Crime>?> GetAll() {
            List<CrimeEntity> entites = await this._context.Crimes.AsNoTracking().ToListAsync();

            List<Crime> crimes = new List<Crime>();
            foreach (CrimeEntity entity in entites) crimes.Add(await GetCrimeModel(entity));

            return crimes;
        }

        public async Task<bool> Exists(Crime model) {
            return await this._context.Crimes.AnyAsync(m =>
                m.Grade.Name.Equals(model.Grade.Name, StringComparison.OrdinalIgnoreCase) &&
                m.Type.Name.Equals(model.Type.Name, StringComparison.OrdinalIgnoreCase) &&
                m.DateStart == model.DateStart &&
                m.DateEnd == model.DateEnd &&
                m.Status == model.Status &&
                m.AddressId == model.Address.Id
            );
        }

        public async Task<CrimeGrade?> GetGradeByName(int gradeId) {
            CrimeGradeEntity? entity = await this._context.CrimeGrades
                .FirstOrDefaultAsync(m => m.CrimeGradeId == gradeId);
            if(entity  == null) return null;
            else return CrimeGradeMapper.ToDomain(entity);
        }

        public async Task<CrimeType?> GetTypeByName(int typeId) { 
            CrimeTypeEntity? entity = await this._context.CrimeTypes
                .FirstOrDefaultAsync(m => m.CrimeTypeId == typeId);
            if (entity == null) return null;
            else return CrimeTypeMapper.ToDomain(entity);
        }

        public async Task Add(Crime crime) {
            await this._context.Crimes.AddAsync(await this.GetCrimeEntity(crime));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el crimen en la base de datos");
        }
            
        
        public async Task Update(Crime crime) {
            this._context.Crimes.Update(await this.GetCrimeEntity(crime));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el crimen en la base de datos");
        }
            
        public async Task Delete(Crime crime) {
            this._context.Crimes.Remove(await this.GetCrimeEntity(crime));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el crimen en la base de datos");

        }
           
        public async Task DeleteRange(List<Crime> models) {
            List<CrimeEntity> entities = new List<CrimeEntity>();

            foreach (Crime model in models) entities.Add(await this.GetCrimeEntity(model));
                
            this._context.Crimes.RemoveRange(entities);
            int rowsAffected = await this._context.SaveChangesAsync();
            if(rowsAffected == 0) throw new InfrastructureException("Error al eliminar los crímenes en la base de datos");
        }

        private async Task<List<UserEntity>> GetUsersOfCrime(List<User> users) {
            List<UserEntity> userEntities = new List<UserEntity>();
            
            foreach (User user in users) userEntities.Add(UserMapper.ToEntity(user));

            return userEntities;
        }

        private async Task<List<CriminalEntity>> GetCriminalOfCrime(List<Criminal> criminals) {
            List<CriminalEntity> criminalEntities = new List<CriminalEntity>();

            foreach (Criminal criminal in criminals) criminalEntities.Add(CriminalMapper.ToEntity(criminal));

            return criminalEntities;
        }

        private async Task<CrimeEntity> GetCrimeEntity(Crime crime) {
            List<UserEntity> userEntities = await GetUsersOfCrime(crime.Users);
            List<CriminalEntity> criminalEntities = await GetCriminalOfCrime(crime.Criminals);

            return CrimeMapper.ToEntity(crime, userEntities, criminalEntities);
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
    }
}
