using Application.Contracts.Requests.Criminal;
using Application.Contracts.Responses;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface ICriminalService
    {
        Task<CriminalResponse> GetById(int id);
        Task<List<CriminalResponse>> GetAll();
        Task<List<CrimeResponse>> GetCriminalCrimes(int id);
        Task<CriminalResponse> Create(CreateCriminalRequest request);
        Task<CriminalResponse> Update(UpdateCriminalRequest request);
        Task Delete(int id);
    }
}
