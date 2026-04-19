using Application.Contracts.Requests.Address;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class AddressService: IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            this._addressRepository = addressRepository;
        }

        public async Task<AddressResponse> GetById(int id)
        {
            Address? model = await this._addressRepository.GetById(id);
            if (model == null) throw new NotFoundException("La dirección no existe");
            return AddressMapper.ToResponse(model);
        }
        
        public async Task<List<AddressResponse>> GetAll()
        {
            List<AddressResponse> viewModels = new List<AddressResponse>();
            List<Address>? models = await this._addressRepository.GetAll();
                
            if (models == null) throw new NotFoundException("No se han podido obtener las direcciones");
                
            foreach (Address model in models) viewModels.Add(AddressMapper.ToResponse(model));
            return viewModels;
        }

        public async Task Create(CreateAddressRequest request, string image)
        {
            Address model = AddressMapper.ToModel(request, image);

            await this._addressRepository.Add(model);
        }

        public async Task Update(UpdateAddressRequest request, string image)
        {
            Address? model = await this._addressRepository.GetById(request.Id);
            if(model == null) throw new NotFoundException("La dirección no existe");

            Address newAddress = AddressMapper.ToModel(request, image);

            await this._addressRepository.Update(model);
        }

        public async Task Delete(int id)
        {
            Address? model = await this._addressRepository.GetById(id);
            if (model == null) throw new NotFoundException("La direccion no existe");

            await this._addressRepository.Delete(model);
        }
    }
}
