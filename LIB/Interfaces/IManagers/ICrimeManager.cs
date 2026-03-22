using LIB.DTOs.Crime;
using LIB.ViewModels;

namespace LIB.Interfaces.IManagers
{
    public interface ICrimeManager
    {
        Task<CrimeViewModel> GetById(int id);
        Task<List<CrimeViewModel>> GetAll();
        Task Create(CreateCrimeRequest dto);
        Task Update(UpdateCrimeRequest dto);
        Task Delete(int id);
        Task<int> Solved(int idCrime);
    }
}
