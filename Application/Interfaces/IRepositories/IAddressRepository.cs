using LIB.Models;
using LIB.ViewModels;

namespace Application.Interfaces.IRepositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetById(int id);
        Task<List<Address>?> GetAll();
        Task<Address?> Exists(AddressViewModel address);
        Task Add(Address address);
        void Update(Address address);
        void Delete(Address address);
        Task<int> SaveChanges();
    }
}
