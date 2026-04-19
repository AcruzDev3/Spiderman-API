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
            AddressEntity? entity = await this._context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AddressId == id);
            if (entity == null) return null;
            else return AddressMapper.ToDomain(entity);
        }

        public async Task<List<Address>?> GetAll() {
            List<AddressEntity> entities = await this._context.Addresses.ToListAsync();

            List<Address> addresses = new List<Address>();
            foreach(AddressEntity entity in entities) addresses.Add(AddressMapper.ToDomain(entity));

            return addresses;
        }

        public async Task<bool> Exists(Address model) {
            try {
                AddressEntity? entity = await this._context.Addresses
                   .FirstOrDefaultAsync(m => m.Street.Equals(model.Street, StringComparison.OrdinalIgnoreCase) &&
                   m.ZipCode.Equals(model.ZipCode, StringComparison.OrdinalIgnoreCase) &&
                   m.Side.Equals(model.Side.ToString(), StringComparison.OrdinalIgnoreCase) &&
                   m.Number == model.Number
                );
                if (entity == null) return false;
                else return true;
            } catch (Exception) {
                throw;
            }
        }

        public async Task Add(Address address) {
            await this._context.Addresses.AddAsync(AddressMapper.ToEntity(address));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al añadir la dirección en la base de datos");
        }

        public async Task Update(Address address) {
            this._context.Addresses.Update(AddressMapper.ToEntity(address));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al actualizar la dirección en la base de datos");
        }

        public async Task Delete(Address address) {
            this._context.Addresses.Remove(AddressMapper.ToEntity(address));
            int rowsAffected = await this._context.SaveChangesAsync();
            if (rowsAffected != 1) throw new InfrastructureException("Error al eliminar la dirección en la base de datos");
        }
    }
}
