using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICriminalService
    {
        Task<Criminal> GetById(int id);
        Task<List<Criminal>> GetAll();
        Task Create(Criminal model);
        Task Update(Criminal model);
        Task Delete(int id);
    }
}
