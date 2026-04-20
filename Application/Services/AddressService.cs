using Application.Constans;
using Application.Contracts.Requests.Address;
using Application.Contracts.Responses;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Interfaces.IRepositories;
using Domain.Models;

namespace Application.Services
{
    public class AddressService: IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAzureImageService _azureImageService;
        public AddressService(IAddressRepository addressRepository, IAzureImageService azureImageService)
        {
            this._addressRepository = addressRepository;
            this._azureImageService = azureImageService;
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

        public async Task<AddressResponse> Create(CreateAddressRequest request)
        {
            string urlImage = DefaultImagesPath.Address;
            if (request.Image != null) {
                urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Addresses.ToString().ToLower(),
                    request.Image.ContentType
                );
            }

            Address model = AddressMapper.ToModel(request, urlImage);
            return AddressMapper.ToResponse(await this._addressRepository.Add(model));
        }

        public async Task<AddressResponse> Update(UpdateAddressRequest request)
        {
            Address? model = await this._addressRepository.GetById(request.Id);
            if(model == null) throw new NotFoundException("La dirección no existe");

            string urlImage = model.Image;

            if (request.Image != null) {
                urlImage = await this._azureImageService.UploadImageAsync(
                    request.Image.OpenReadStream(),
                    FolderImageEnum.Addresses.ToString().ToLower(),
                    request.Image.ContentType
                );
                await this._azureImageService.DeleteAsync(model.Image);
            }
            Address newAddress = AddressMapper.ToModel(request, urlImage);

            Address address = await this._addressRepository.Update(model);
            return AddressMapper.ToResponse(address);
        }

        public async Task Delete(int id)
        {
            Address? model = await this._addressRepository.GetById(id);
            if (model == null) throw new NotFoundException("La direccion no existe");

            await this._addressRepository.Delete(model);
        }
    }
}
