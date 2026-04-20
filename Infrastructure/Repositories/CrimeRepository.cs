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
            try {
                CrimeEntity? entity = await this._context.Crimes
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(c => c.CrimeId == id);

                if (entity == null) return null;
                else return await this.GetCrimeModel(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el crimen con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<Crime>?> GetAll() {
            try {
                List<CrimeEntity> entites = await this._context.Crimes.AsNoTracking().ToListAsync();

                List<Crime> crimes = new List<Crime>();
                foreach (CrimeEntity entity in entites) crimes.Add(await GetCrimeModel(entity));

                return crimes;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los crímenes de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(Crime model) {
            try {
                return await this._context.Crimes.AnyAsync(m =>
                m.Grade.Name.Equals(model.Grade.Name, StringComparison.OrdinalIgnoreCase) &&
                m.Type.Name.Equals(model.Type.Name, StringComparison.OrdinalIgnoreCase) &&
                m.DateStart == model.DateStart &&
                m.DateEnd == model.DateEnd &&
                m.Status == model.Status &&
                m.AddressId == model.Address.Id
            );
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al verificar la existencia del crimen en la base de datos: {ex.Message}");
            }
            
        }

        public async Task<CrimeGrade?> GetGradeByName(int gradeId) {
            try {
                CrimeGradeEntity? entity = await this._context.CrimeGrades
                .FirstOrDefaultAsync(m => m.CrimeGradeId == gradeId);
                if (entity == null) return null;
                else return CrimeGradeMapper.ToDomain(entity);
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener el grado de crimen con id {gradeId} de la base de datos: {ex.Message}");
            }
            
        }

        public async Task<CrimeType?> GetTypeByName(int typeId) { 
            try {
                CrimeTypeEntity? entity = await this._context.CrimeTypes
                .FirstOrDefaultAsync(m => m.CrimeTypeId == typeId);
                if (entity == null) return null;
                else return CrimeTypeMapper.ToDomain(entity);
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener el tipo de crimen con id {typeId} de la base de datos: {ex.Message}");
            }
        }

        public async Task<Crime> Add(Crime crime) {
            try {
                CrimeEntity entity = await this.GetCrimeEntity(crime);
                await this._context.Crimes.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el crimen en la base de datos");
                return await this.GetCrimeModel(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el crimen en la base de datos: {ex.Message}");
            }
        }

        public async Task<Crime> Update(Crime crime) {
            try {
                CrimeEntity entity = await this.GetCrimeEntity(crime);
                this._context.Crimes.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el crimen en la base de datos");
                return await this.GetCrimeModel(entity);    
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al actualizar el crimen en la base de datos: {ex.Message}");
            }
        }
            
        public async Task Delete(Crime crime) {
            try {
                this._context.Crimes.Remove(await this.GetCrimeEntity(crime));
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el crimen en la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el crimen en la base de datos: {ex.Message}");
            }
        }
           
        public async Task DeleteRange(List<Crime> models) {
            try {
                List<CrimeEntity> entities = new List<CrimeEntity>();

                foreach (Crime model in models) entities.Add(await this.GetCrimeEntity(model));

                this._context.Crimes.RemoveRange(entities);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected == 0) throw new InfrastructureException("Error al eliminar los crímenes en la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar los crímenes en la base de datos: {ex.Message}");
            }
        }

        private async Task<List<UserEntity>> GetUsersOfCrime(List<User> users) {
            try {
                List<UserEntity> userEntities = new List<UserEntity>();

                foreach (User user in users) userEntities.Add(UserMapper.ToEntity(user));

                return userEntities;
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener los héroes del crimen de la base de datos: {ex.Message}");
            }
        }

        private async Task<List<CriminalEntity>> GetCriminalOfCrime(List<Criminal> criminals) {
            try {
                List<CriminalEntity> criminalEntities = new List<CriminalEntity>();

                foreach (Criminal criminal in criminals) criminalEntities.Add(CriminalMapper.ToEntity(criminal));

                return criminalEntities;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener los criminales del crimen de la base de datos: {ex.Message}");
            }
            
        }

        private async Task<CrimeEntity> GetCrimeEntity(Crime crime) {
            try {
                List<UserEntity> userEntities = await GetUsersOfCrime(crime.Users);
                List<CriminalEntity> criminalEntities = await GetCriminalOfCrime(crime.Criminals);

                return CrimeMapper.ToEntity(crime, userEntities, criminalEntities);
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener la entidad del crimen de la base de datos: {ex.Message}");
            }
        }

        private async Task<Crime> GetCrimeModel(CrimeEntity entity) {
            try {
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
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el modelo del crimen de la base de datos: {ex.Message}");
            }
        }
    }
}
