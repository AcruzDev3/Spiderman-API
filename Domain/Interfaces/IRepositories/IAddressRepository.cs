using Domain.Models;

namespace Domain.Interfaces.IRepositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetById(int id);
        Task<List<Address>?> GetAll();
        Task Add(Address address);
        void Update(Address address);
        void Delete(Address address);
        Task<int> SaveChanges();
    }
}
