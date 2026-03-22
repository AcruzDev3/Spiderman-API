using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly SpidermanContext _context;
        public AddressRepository(SpidermanContext context) {
            this._context = context;
        }

        public async Task<Address?> GetById(int id) {
            return await this._context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AddressId == id);
        }

        public async Task<List<Address>?> GetAll() {
            return await this._context.Addresses
                .ToListAsync();
        }

        public async Task<Address?> Exists(AddressViewModel viewModel) {
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

        public async Task Add(Address address) {
            await this._context.Addresses.AddAsync(address);
        }

        public void Update(Address address) {
            this._context.Addresses.Update(address);
        }

        public void Delete(Address address) {
            this._context.Addresses.Remove(address);
        }

        public async Task<int> SaveChanges() {
            return await this._context.SaveChangesAsync();
        }
    }
}
