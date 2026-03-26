using LIB.DTOs.Criminal;
using LIB.ViewModels;

namespace Application.Interfaces.Services
{
    public interface ICriminalService
    {
        Task<CriminalViewModel> GetById(int id);
        Task<List<CriminalViewModel>> GetAll();
        Task Create(CreateCriminalRequest dto);
        Task Update(UpdateCriminalRequest dto);
        Task Delete(int id);
    }
}
