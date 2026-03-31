using Application.Contracts.Requests.Crime;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICrimeService
    {
        Task<Crime> GetById(int id);
        Task<List<Crime>> GetAll();
        Task Create(CreateCrimeRequest request);
        Task Update(UpdateCrimeRequest request);
        Task Delete(int id);
        Task<int> Solved(int idCrime);
    }
}
