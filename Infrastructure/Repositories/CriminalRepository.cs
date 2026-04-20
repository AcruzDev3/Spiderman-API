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
            try {
                CriminalEntity? entity = await this._context.Criminals.AsNoTracking()
                                        .FirstOrDefaultAsync(c => c.CriminalId == id);
                if (entity == null) return null;
                else return await this.GetCriminalModel(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el criminal con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<Criminal>?> GetByIds(List<int> ids) {
            try {
                List<CriminalEntity>? entities = await this._context.Criminals
                .Where(c => ids.Contains(c.CriminalId))
                .ToListAsync();
                List<Criminal> criminals = new List<Criminal>();

                foreach (CriminalEntity entity in entities) criminals.Add(await this.GetCriminalModel(entity));

                return criminals;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener los criminales con ids {string.Join(", ", ids)} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<Criminal>?> GetAll() {
            try {
                List<CriminalEntity> entites = await this._context.Criminals.AsNoTracking()
                                            .ToListAsync();
                List<Criminal> criminals = new List<Criminal>();
                foreach (CriminalEntity entite in entites) criminals.Add(await this.GetCriminalModel(entite));

                return criminals;
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los criminales de la base de datos: {ex.Message}");
            }
            
        }

        public async Task<List<Crime>?> GetCrimes(Criminal model) {
            try {
                List<CrimeEntity>? crimesEntities = await this._context.Crimes
                .Where(c => c.Criminals.Any(cr => cr.CriminalId == model.Id))
                .ToListAsync();

                List<Crime> crimes = new List<Crime>();
                foreach (CrimeEntity crimeEntity in crimesEntities)
                    crimes.Add(await this.GetCrimeModel(crimeEntity));

                return crimes;
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al obtener los crímenes del criminal con id {model.Id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(string name) {
            try { 
                CriminalEntity? entity = await this._context.Criminals
                            .FirstOrDefaultAsync(m => m.Name
                            .Equals(name, StringComparison.OrdinalIgnoreCase));

                if (entity == null) return false;
                else return true;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al verificar la existencia del criminal con nombre {name} en la base de datos: {ex.Message}");
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
                throw new InfrastructureException($"Error al obtener el modelo de crimen con id {entity.CrimeId} de la base de datos: {ex.Message}");
            }
        }

        public async Task<CriminalRiskLevel?> GetCriminalRiskLevelAsync(int idRisk) {
            try {
                CriminalRiskLevelEntity? entity = await this._context.CriminalRiskLevels
                    .FirstOrDefaultAsync(r => r.CriminalRiskLevelId == idRisk);
                if (entity != null) return CriminalRiskLevelMapper.ToDomain(entity);
                else return null;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el nivel de riesgo del criminal con id {idRisk} de la base de datos: {ex.Message}");
            }
        }

        public async Task<Criminal> Add(Criminal criminal) {
            try {
                CriminalEntity? entity = CriminalMapper.ToEntity(criminal);
                await this._context.Criminals.AddAsync(entity);
                
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el criminal en base de datos");

                return CriminalMapper.ToDomain(
                    entity,
                    CriminalRiskLevelMapper.ToDomain(entity.Risk)
                );
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el criminal con id {criminal.Id} en la base de datos: {ex.Message}");
            }
        }

        public async Task<Criminal> Update(Criminal criminal) {
            try {
                CriminalEntity entity = CriminalMapper.ToEntity(criminal);
                this._context.Criminals.Update(entity);

                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el criminal en base de datos");

                return CriminalMapper.ToDomain(
                    entity,
                    CriminalRiskLevelMapper.ToDomain(entity.Risk)
                );
            } catch(Exception ex) {
                throw new InfrastructureException($"Error al actualizar el criminal con id {criminal.Id} en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(Criminal criminal) {
            try {
                this._context.Criminals.Remove(CriminalMapper.ToEntity(criminal));
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el criminal en base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el criminal con id {criminal.Id} en la base de datos: {ex.Message}");
            }
            
        }

        private async Task<Criminal> GetCriminalModel(CriminalEntity entity) {
            try { 
                CriminalRiskLevel risk = CriminalRiskLevelMapper.ToDomain(entity.Risk);
                return CriminalMapper.ToDomain(entity, risk);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el modelo de criminal con id {entity.CriminalId} de la base de datos: {ex.Message}");
            }
        }
    }
}
