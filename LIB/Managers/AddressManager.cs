using LIB.DTOs.Address;
using LIB.Interfaces.IManagers;
using LIB.Interfaces.IRepositories;
using LIB.Models;
using LIB.ViewModels;
namespace LIB.Managers
{
    public class AddressManager: IAddressManager
    {
        private readonly IAddressRepository _addressRepository;
        public AddressManager(IAddressRepository addressRepository)
        {
            this._addressRepository = addressRepository;
        }

        public async Task<AddressViewModel> GetById(int id)
        {
            AddressViewModel? addresViewModel = null;
            try
            {
                Address? model = await this._addressRepository.GetById(id);
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
                List<Address>? models = await this._addressRepository.GetAll();
                
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

                await this._addressRepository.Add(model);
                int rowsAffected = await this._addressRepository.SaveChanges();
                if (rowsAffected != 1) throw new Exception("No se ha podido crear la direccion");
            }
            catch (Exception) 
            {
                throw;            
            }
        }

        public async Task Update(UpdateAddressRequest dto)
        {
            try
            {
                if(dto == null) throw new Exception("La dirección no es válida");
                
                Address? model = await this._addressRepository.GetById(dto.Id);
                if(model == null) throw new Exception("La dirección no existe");

                this._addressRepository.Update(model);
                int rowsAffected = await this._addressRepository.SaveChanges();
                
                if (rowsAffected != 1) throw new Exception("No se ha podido actualizar la direccion");
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
                Address? model = await this._addressRepository.GetById(id);
                if (model == null) throw new Exception("La direccion no existe");

                this._addressRepository.Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
