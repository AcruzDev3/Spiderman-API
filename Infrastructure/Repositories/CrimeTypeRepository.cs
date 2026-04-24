using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CrimeTypeRepository : ICrimeTypeRepository
    {
        private readonly SpidermanContext _context;

        public CrimeTypeRepository(SpidermanContext context) {
            _context = context;
        }

        public async Task<CrimeType?> GetById(int id) {
            try {
                CrimeTypeEntity? entity = await this._context.CrimeTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.CrimeTypeId == id);

                if (entity == null) return null;
                else return CrimeTypeMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el tipo de crimen con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<CrimeType>?> GetAll() {
            try {
                List<CrimeTypeEntity> entities = await this._context.CrimeTypes
                                    .ToListAsync();
                if (entities == null || entities.Count == 0) return null;
                List<CrimeType> crimeTypes = new List<CrimeType>();
                foreach (CrimeTypeEntity entity in entities) crimeTypes.Add(CrimeTypeMapper.ToDomain(entity));
                return crimeTypes;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los tipos de crimen de la base de datos: {ex.Message}");
            }
        }

        public async Task<CrimeType> Add(CrimeType model) {
            try {
                CrimeTypeEntity entity = CrimeTypeMapper.ToEntity(model);
                await this._context.CrimeTypes.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el tipo de crimen a la base de datos");
                return CrimeTypeMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el tipo de crimen a la base de datos: {ex.Message}");
            }
        }

        public async Task<CrimeType> Update(CrimeType model) {
            try {
                CrimeTypeEntity entity = CrimeTypeMapper.ToEntity(model);
                this._context.CrimeTypes.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el tipo de crimen en la base de datos");
                return CrimeTypeMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al actualizar el tipo de crimen en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(CrimeType model) {
            try {
                CrimeTypeEntity entity = CrimeTypeMapper.ToEntity(model);
                this._context.CrimeTypes.Remove(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el tipo de crimen de la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el tipo de crimen de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(string name) {
            try {
                return await this._context.CrimeTypes.AnyAsync(r => r.Name == name);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al comprobar si el tipo de crimen con nombre {name} existe en la base de datos: {ex.Message}");
            }
        }
    }
}
