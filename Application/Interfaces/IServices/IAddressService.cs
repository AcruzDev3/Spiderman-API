using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<Address> GetById(int id);
        Task<List<Address>> GetAll();
        Task Create(Address model);
        Task Update(Address model);
        Task Delete(int id);
    }
}
