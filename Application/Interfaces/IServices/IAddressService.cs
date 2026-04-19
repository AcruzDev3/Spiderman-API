using Application.Contracts.Requests.Address;
using Application.Contracts.Responses;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<AddressResponse> GetById(int id);
        Task<List<AddressResponse>> GetAll();
        Task Create(CreateAddressRequest request, string image);
        Task Update(UpdateAddressRequest request, string image);
        Task Delete(int id);
    }
}
