using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetById(int id);
        Task<List<Address>?> GetAll();
        Task<Address> Add(Address address);
        Task<Address> Update(Address address);
        Task Delete(Address address);
        Task<bool> Exists(Address address);
    }
}
