using LIB.DTOs;
using LIB.Enums;
using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace LIB.Managers
{
    public class AddressManager: IManager<AddressViewModel, CreateAddressRequest, Address>
    {
        private readonly SpidermanContext _context;
        public AddressManager(SpidermanContext context)
        {
            this._context = context;
        }

        public async Task<AddressViewModel> GetById(int id)
        {
            AddressViewModel? addresViewModel = null;
            try
            {
                Address? model = await this.GetModel(id);
                if (model == null) throw new Exception("No se pudo encontrar la direccion");
                addresViewModel = new AddressViewModel(model);  
            }
            catch(Exception)
            {
                throw;
            }
            return addresViewModel; 
        }
        
        public async Task<List<AddressViewModel>> GetAll()
        {
            List<AddressViewModel> viewModels = new List<AddressViewModel>();
            try {
                List<Address>? models = await this.GetAllModels();
                
                if (models == null) throw new Exception("No se han podido obtener las direcciones");
                
                foreach (Address model in models) viewModels.Add(new AddressViewModel(model));
            }
            catch (Exception) 
            {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateAddressRequest dto)
        {
            try
            {
                if(dto == null) throw new Exception("La dirección no es válida");

                AddressViewModel viewModel = new AddressViewModel(dto);
                if(viewModel == null) throw new Exception("El modelo vista de la dirección no es válido");

                Address model = new Address(viewModel);
                if (model == null) throw new Exception("El modelo de la dirección no es válido");

                await this._context.Addresses.AddAsync(model);
                int rowsAffected = await this._context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se ha podido crear la direccion");
                
            }
            catch (Exception) 
            {
                throw;            
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                Address? model = await this.GetModel(id);
                if (model == null) throw new Exception("La direccion no existe");

                this._context.Addresses.Remove(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Address?> Exists(AddressViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("El modelo vista de la dirección no es válido");

                 return await this._context.Addresses
                    .FirstOrDefaultAsync(m => m.Street.Equals(viewModel.Street, StringComparison.CurrentCultureIgnoreCase) &&
                    m.ZipCode.Equals(viewModel.ZipCode, StringComparison.CurrentCultureIgnoreCase) &&
                    m.Side.Equals(viewModel.Side.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
                    m.Number == viewModel.Number
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<Address?> GetModel(int id)
        {
            return await this._context.Addresses.FirstOrDefaultAsync(a => a.AddressId == id);
        }

        private async Task<List<Address>> GetAllModels()
        {
            return await this._context.Addresses.AsNoTracking().ToListAsync();
        }
    }
}
