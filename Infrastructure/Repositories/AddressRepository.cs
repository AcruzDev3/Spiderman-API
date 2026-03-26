
using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.EF_Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly SpidermanContext _context;
        public AddressRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<AddressEntity?> GetById(int id) {
            return await this._context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AddressId == id);
        }

        public async Task<List<AddressEntity>?> GetAll() {
            return await this._context.Addresses
                .ToListAsync();
        }

        public async Task<Address?> Exists(Address viewModel) {
            try {
                if (viewModel == null) throw new Exception("El modelo vista de la dirección no es válido");

                return await this._context.Addresses
                   .FirstOrDefaultAsync(m => m.Street.Equals(viewModel.Street, StringComparison.OrdinalIgnoreCase) &&
                   m.ZipCode.Equals(viewModel.ZipCode, StringComparison.OrdinalIgnoreCase) &&
                   m.Side.Equals(viewModel.Side.ToString(), StringComparison.OrdinalIgnoreCase) &&
                   m.Number == viewModel.Number
                   );
            } catch (Exception) {
                throw;
            }
        }

        public async Task Add(AddressEntity address) {
            await this._context.Addresses.AddAsync(address);
        }

        public void Update(AddressEntity address) {
            this._context.Addresses.Update(address);
        }

        public void Delete(AddressEntity address) {
            this._context.Addresses.Remove(address);
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }
    }
}
