using Application.Contracts.Requests.Address;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<Address> GetById(int id);
        Task<List<Address>> GetAll();
        Task Create(CreateAddressRequest request);
        Task Update(UpdateAddressRequest request);
        Task Delete(int id);
    }
}
