using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICrimeService
    {
        Task<Crime> GetById(int id);
        Task<List<Crime>> GetAll();
        Task Create(Crime model);
        Task Update(Crime model);
        Task Delete(int id);
        Task<int> Solved(int idCrime);
    }
}
