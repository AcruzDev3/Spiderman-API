using Application.Contracts.Requests.Address;
using Application.Contracts.Responses;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<AddressResponse> GetById(int id);
        Task<List<AddressResponse>> GetAll();
        Task<AddressResponse> Create(CreateAddressRequest request);
        Task<AddressResponse> Update(UpdateAddressRequest request);
        Task Delete(int id);
    }
}
