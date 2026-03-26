using LIB.DTOs.Crime;
using LIB.ViewModels;

namespace Application.Interfaces.Services
{
    public interface ICrimeService
    {
        Task<CrimeViewModel> GetById(int id);
        Task<List<CrimeViewModel>> GetAll();
        Task Create(CreateCrimeRequest dto);
        Task Update(UpdateCrimeRequest dto);
        Task Delete(int id);
        Task<int> Solved(int idCrime);
    }
}
