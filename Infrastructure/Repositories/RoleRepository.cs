using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SpidermanContext _context;

        public RoleRepository(SpidermanContext context) {
            _context = context;
        }

        public async Task<Role?> GetById(int id) {
            try {
                RoleEntity? entity = await this._context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoleId == id);

                if (entity == null) return null;
                else return RoleMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el rol con id {id} de la base de datos: {ex.Message}");
            }
        }

        public async Task<List<Role>?> GetAll() {
            try {
                List<RoleEntity> entities = await this._context.Roles
                                    .ToListAsync();
                if (entities == null || entities.Count == 0) return null;
                List<Role> roles = new List<Role>();
                foreach (RoleEntity entity in entities) roles.Add(RoleMapper.ToDomain(entity));
                return roles;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todos los roles de la base de datos: {ex.Message}");
            }
        }

        public async Task<Role?> GetNeighbourRole() {
            try {   
                RoleEntity? entity = await this._context.Roles
                    .Where(r => r.Name == "NEIGHBOUR")
                    .FirstOrDefaultAsync();

                if (entity == null) return null;

                else return RoleMapper.ToDomain(entity);

            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener el rol de vecino la base de datos: {ex.Message}");
            }
        }

        public async Task<Role> Add(Role model) {
            try {
                RoleEntity entity = RoleMapper.ToEntity(model);
                await this._context.Roles.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir el rol a la base de datos");
                return RoleMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir el rol a la base de datos: {ex.Message}");
            }
        }

        public async Task<Role> Update(Role model) {
            try {
                RoleEntity entity = RoleMapper.ToEntity(model);
                this._context.Roles.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar el rol en la base de datos");
                return RoleMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al actualizar el rol en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(Role model) {
            try {
                RoleEntity entity = RoleMapper.ToEntity(model);
                this._context.Roles.Remove(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar el rol de la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar el rol de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(string name) {
            try {
                return await this._context.Roles.AnyAsync(r => r.Name == name);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al comprobar si el rol con nombre {name} existe en la base de datos: {ex.Message}");
            }
        }
    }
}
