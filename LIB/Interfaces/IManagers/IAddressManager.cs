using LIB.DTOs.Address;
using LIB.ViewModels;

namespace LIB.Interfaces.IManagers
{
    public interface IAddressManager
    {
        Task<AddressViewModel> GetById(int id);
        Task<List<AddressViewModel>> GetAll();
        Task Create(CreateAddressRequest dto);
        Task Update(UpdateAddressRequest dto);
        Task Delete(int id);
    }
}
