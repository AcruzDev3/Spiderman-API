using Application.Contracts.Requests.Criminal;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICriminalService
    {
        Task<Criminal> GetById(int id);
        Task<List<Criminal>> GetAll();
        Task Create(CreateCriminalRequest request);
        Task Update(UpdateCriminalRequest request);
        Task Delete(int id);
    }
}
