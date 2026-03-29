
using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
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
            return AddressMapper.ToDomain(entity);
        }

        public async Task<List<Address>?> GetAll() {
            List<AddressEntity> entities = await this._context.Addresses.ToListAsync();
            List<Address> addresses = new List<Address>();
            foreach(AddressEntity entity in entities)
                addresses.Add(AddressMapper.ToDomain(entity));
            return addresses;
        }

        public async Task<Address?> Exists(Address viewModel) {
            try {
                if (viewModel == null) throw new Exception("El modelo vista de la dirección no es válido");

                AddressEntity? entity = await this._context.Addresses
                   .FirstOrDefaultAsync(m => m.Street.Equals(viewModel.Street, StringComparison.OrdinalIgnoreCase) &&
                   m.ZipCode.Equals(viewModel.ZipCode, StringComparison.OrdinalIgnoreCase) &&
                   m.Side.Equals(viewModel.Side.ToString(), StringComparison.OrdinalIgnoreCase) &&
                   m.Number == viewModel.Number
                );

                return AddressMapper.ToDomain(entity);
            } catch (Exception) {
                throw;
            }
        }

        public async Task Add(Address address) {
            await this._context.Addresses.AddAsync(AddressMapper.ToEntity(address));
        }

        public void Update(Address address) {
            this._context.Addresses.Update(AddressMapper.ToEntity(address));
        }

        public void Delete(Address address) {
            this._context.Addresses.Remove(AddressMapper.ToEntity(address));
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }
    }
}
