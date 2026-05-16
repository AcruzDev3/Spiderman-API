using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CriminalRiskLevelRepository : ICriminalRiskLevelRepository
    {
        private readonly SpidermanContext _context;

        public CriminalRiskLevelRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<CriminalRiskLevel?> GetById(int id) {
            try {
                CriminalRiskLevelEntity? entity = await this._context.CriminalRiskLevels
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.CriminalRiskLevelId == id);

                if (entity == null) return null;
                else return CriminalRiskLevelMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el rol con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<CriminalRiskLevel>?> GetAll() {
            try {
                List<CriminalRiskLevelEntity> entities = await this._context.CriminalRiskLevels
                                    .ToListAsync();
                if (entities == null || entities.Count == 0) return null;
                List<CriminalRiskLevel> criminalRiskLevels = new List<CriminalRiskLevel>();
                foreach (CriminalRiskLevelEntity entity in entities) criminalRiskLevels.Add(CriminalRiskLevelMapper.ToDomain(entity));
                return criminalRiskLevels;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los roles de la base de datos: {ex.Message}");
            }
        }

        public async Task<CriminalRiskLevel> Add(CriminalRiskLevel model) {
            try {
                CriminalRiskLevelEntity entity = CriminalRiskLevelMapper.ToEntity(model);
                await this._context.CriminalRiskLevels.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el rol a la base de datos");
                return CriminalRiskLevelMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el rol a la base de datos: {ex.Message}");
            }
        }

        public async Task<CriminalRiskLevel> Update(CriminalRiskLevel model) {
            try {
                CriminalRiskLevelEntity entity = CriminalRiskLevelMapper.ToEntity(model);
                this._context.CriminalRiskLevels.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el rol en la base de datos");
                return CriminalRiskLevelMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al actualizar el rol en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(CriminalRiskLevel model) {
            try {
                CriminalRiskLevelEntity entity = CriminalRiskLevelMapper.ToEntity(model);
                this._context.CriminalRiskLevels.Remove(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el rol de la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el rol de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(string name) {
            try {
                return await this._context.CriminalRiskLevels.AnyAsync(r => r.Name == name);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al comprobar si el rol con nombre {name} existe en la base de datos: {ex.Message}");
            }
        }
    }
}
