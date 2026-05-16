using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CrimeGradeRepository : ICrimeGradeRepository
    {
        private readonly SpidermanContext _context;

        public CrimeGradeRepository(SpidermanContext context) {
            _context = context;
        }

        public async Task<CrimeGrade?> GetById(int id) {
            try {
                CrimeGradeEntity? entity = await this._context.CrimeGrades
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.CrimeGradeId == id);

                if (entity == null) return null;
                else return CrimeGradeMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el grado de crimen con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<CrimeGrade>?> GetAll() {
            try {
                List<CrimeGradeEntity> entities = await this._context.CrimeGrades
                                    .ToListAsync();
                if (entities == null || entities.Count == 0) return null;
                List<CrimeGrade> crimeGrades = new List<CrimeGrade>();
                foreach (CrimeGradeEntity entity in entities) crimeGrades.Add(CrimeGradeMapper.ToDomain(entity));
                return crimeGrades;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los grados de crimen de la base de datos: {ex.Message}");
            }
        }

        public async Task<CrimeGrade> Add(CrimeGrade model) {
            try {
                CrimeGradeEntity entity = CrimeGradeMapper.ToEntity(model);
                await this._context.CrimeGrades.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el grado de crimen a la base de datos");
                return CrimeGradeMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el grado de crimen a la base de datos: {ex.Message}");
            }
        }

        public async Task<CrimeGrade> Update(CrimeGrade model) {
            try {
                CrimeGradeEntity entity = CrimeGradeMapper.ToEntity(model);
                this._context.CrimeGrades.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el grado de crimen en la base de datos");
                return CrimeGradeMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al actualizar el grado de crimen en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(CrimeGrade model) {
            try {
                CrimeGradeEntity entity = CrimeGradeMapper.ToEntity(model);
                this._context.CrimeGrades.Remove(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el grado de crimen de la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el grado de crimen de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(string name) {
            try {
                return await this._context.CrimeGrades.AnyAsync(r => r.Name == name);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al comprobar si el grado de crimen con nombre {name} existe en la base de datos: {ex.Message}");
            }
        }
    }
}
