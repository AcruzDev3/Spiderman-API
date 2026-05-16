using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Infrastructure.Exceptions;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly SpidermanContext _context;
        public AddressRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<Address?> GetById(int id) {
            try {
                AddressEntity? entity = await this._context.Addresses
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.AddressId == id);
                if (entity == null) return null;
                else return AddressMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener la dirección con id {id} de la base de datos: {ex.Message}");
            }
            
        }

        public async Task<List<Address>?> GetAll() {
            try {
                List<AddressEntity> entities = await this._context.Addresses.ToListAsync();

                List<Address> addresses = new List<Address>();
                foreach (AddressEntity entity in entities) addresses.Add(AddressMapper.ToDomain(entity));

                return addresses;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al obtener todas las direcciones de la base de datos: {ex.Message}");
            }
        }

        public async Task<bool> Exists(Address model) {
            try {
                AddressEntity? entity = await this._context.Addresses
                   .FirstOrDefaultAsync(m => m.Street == model.Street &&
                   m.ZipCode == model.ZipCode &&
                   m.Side == model.Side.ToString() &&
                   m.Number == model.Number
                );
                if (entity == null) return false;
                else return true;
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al verificar la existencia de la dirección en la base de datos: {ex.Message}");
            }
        }

        public async Task<Address> Add(Address address) {
            try {
                AddressEntity entity = AddressMapper.ToEntity(address);
                await this._context.Addresses.AddAsync(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al añadir la dirección en la base de datos");
                return AddressMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al añadir la dirección en la base de datos: {ex.Message}");
            }
        }

        public async Task<Address> Update(Address address) {
            try {
                AddressEntity entity = AddressMapper.ToEntity(address);
                this._context.Addresses.Update(entity);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar la dirección en la base de datos");
                return AddressMapper.ToDomain(entity);
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al actualizar la dirección en la base de datos: {ex.Message}");
            }
        }

        public async Task Delete(Address address) {
            try {
                this._context.Addresses.Remove(AddressMapper.ToEntity(address));
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar la dirección en la base de datos");
            } catch (Exception ex) {
                throw new InfrastructureException($"Error al eliminar la dirección en la base de datos: {ex.Message}");
            }
        }
    }
}
